using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed = 3f; // Half of runningSpeed for best animation.
    [SerializeField]
    float runningSpeed = 6f;

    NavMeshAgent agent;
    bool canMove = true;
    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Assign input controls to player movement.
        InputManager.instance.Controls.Player.Move.performed+= ctx => Move(ctx.ReadValue<Vector2>());
        InputManager.instance.Controls.Player.Move.canceled += ctx => Stop();
        InputManager.instance.Controls.Player.Run.performed += ctx => ToggleRun();
    }

    void LateUpdate() {
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVerticle);
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