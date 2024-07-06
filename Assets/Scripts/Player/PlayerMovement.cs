using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : GameBehaviour
{
    [SerializeField]
    private float walkingSpeed = 2.25f; // Half of runningSpeed for best animation.
    [SerializeField]
    private float runningSpeed = 10f;
    [SerializeField]
    private CinemachineBrain cameraBrain;

    private NavMeshAgent agent;
    private bool isFirstPerson = false;
    private bool isRunning = false;
    private float moveVerticle = 0.0f;
    private float moveHorizontal = 0.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Assign input controls to player movement.
        gameManager.Input.Controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        gameManager.Input.Controls.Player.Move.canceled += _ => Stop();
        gameManager.Input.Controls.Player.Run.performed += _ => Run();
        gameManager.Input.Controls.Player.Run.canceled += _ => Walk();

        // Listen to changes in camera state.
        gameManager.Camera.OnToggleView += OnCameraToggled;
    }

    private void LateUpdate()
    {
        if(isFirstPerson)
        {
            MoveFirstPerson();
        }
        else
        {
            MoveThirdPerson();
        }
    }

    private void MoveThirdPerson()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVerticle);
        Vector3 moveDestination = transform.position + movement;
        agent.speed = this.Speed;
        agent.destination = moveDestination;
    }

    private void MoveFirstPerson()
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

    private void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
    }

    private void Stop()
    {
        moveVerticle = 0f;
        moveHorizontal = 0f;
    }

    private void Run()
    {
        isRunning = true;
    }

    private void Walk()
    {
        isRunning = false;
    }

    private void OnCameraToggled()
    {
        isFirstPerson = gameManager.Camera.IsFirstPerson;
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