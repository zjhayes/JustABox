using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed = 2.25f; // Half of runningSpeed for best animation.
    [SerializeField]
    float runningSpeed = 10f;
    [SerializeField]
    CinemachineBrain cameraBrain;

    NavMeshAgent agent;
    bool canMove = true;
    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    bool firstPerson = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Assign input controls to player movement.
        InputManager.Instance.Controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        InputManager.Instance.Controls.Player.Move.canceled += ctx => Stop();
        InputManager.Instance.Controls.Player.Run.performed += ctx => Run();
        InputManager.Instance.Controls.Player.Run.canceled += ctx => Walk();

        InputManager.Instance.Controls.Player.Camera.performed += _ => SwitchView();
    }

    void LateUpdate()
    {
        if(!firstPerson)
        {
            MoveThirdPerson();
        }
        else
        {
            MoveFirstPerson();
        }
    }

    // TO DO: Replace with listener
    void SwitchView()
    {
        firstPerson = !firstPerson;
    }

    void MoveThirdPerson()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVerticle);
        Vector3 moveDestination = transform.position + movement;
        agent.speed = this.Speed;
        agent.destination = moveDestination;
    }

    void MoveFirstPerson()
    {
        // Get currently active camera. TO DO: Set with listener. Get from game manager.
        CinemachineStateDrivenCamera cameraDriver = (CinemachineStateDrivenCamera) cameraBrain.ActiveVirtualCamera;
        CinemachineVirtualCamera camera = (CinemachineVirtualCamera) cameraDriver.LiveChild;
        var forward = camera.Follow.forward;
        var right = camera.Follow.right;
 
        // Project forward and right vectors on the horizontal plane (y = 0).
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
 
        // Calculate destination relative to camera.
        var desiredMoveDirection = forward * moveVerticle + right * moveHorizontal;

        Vector3 movement = new Vector3(desiredMoveDirection.x, 0f, desiredMoveDirection.z);
        Vector3 moveDestination = transform.position + movement;
        agent.speed = this.Speed;
        agent.destination = moveDestination;
    }

    void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
    }

    void Stop()
    {
        moveVerticle = 0f;
        moveHorizontal = 0f;
    }

    void ToggleRun()
    {
        isRunning = !isRunning;
    }

    void Run()
    {
        isRunning = true;
    }

    void Walk()
    {
        isRunning = false;
    }
    
    public float Speed
    {
        get
        {
            return isRunning ? runningSpeed : walkingSpeed;
        }
    }

    public float RunningSpeed
    {
        get
        {
            return runningSpeed;
        }
    }
}