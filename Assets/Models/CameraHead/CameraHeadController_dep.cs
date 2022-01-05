using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CameraHeadController : MonoBehaviour, IController
{
    [SerializeField]
    private float awareDistance = 10.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private Transform[] navPoints;
    [SerializeField]
    private bool isPassive;
    [SerializeField]
    private float searchTime = 5.0f;
    [SerializeField]
    private float attackRange = 5.0f;

    private NavMeshAgent agent;
    private Transform goal;
    private Transform player;
    private Vector3 playerLastPosition;
    private int destinationPoint = 0;
    private float playerDistance;
    private bool pathBlocked = false;
    private float currentSearchTime = 0.0f;

    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;
    readonly float NUMBER_OF_RAYS = 24.0f;
    readonly float SIGHT_OFFSET_BOTTOM = -0.5f;
    readonly float SIGHT_OFFSET_TOP = 1.0f;
    readonly string PLAYER_TAG = "Player";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.Player.transform;
        currentSearchTime = searchTime;

        agent.autoBraking = false;

        if(isPassive)
        {
            //currentState = State.PASSIVE;
        }

    }

    public void Test(){}

    void Update()
    {
        Scan();
    }

    public bool Scan()
    {
        // Check if player is in view.
        return CanSeePlayer(SIGHT_OFFSET_BOTTOM, -60, 5);
    }

    bool CanSeePlayer(float sightOffset, float startAngleOffset, float stepAngleOffset)
    {
        Quaternion startingAngle = Quaternion.AngleAxis(startAngleOffset, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(stepAngleOffset, Vector3.up);
        Quaternion angle = transform.rotation * startingAngle;
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += sightOffset;
        Vector3 forward = angle * Vector3.forward;
        for(int i = 0; i < NUMBER_OF_RAYS; i++)
        {
            if(Physics.Raycast(position, forward, out hit, awareDistance))
            {
                if(hit.collider.tag == PLAYER_TAG)
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    playerLastPosition = player.transform.position;
                    return true;
                }
            }
            forward = stepAngle * forward;
        }
        return false;
    }

    void Patrol()
    {
        if(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE)
        {
            StartCoroutine(WaitAndMoveToNextPoint());
        }
    }

    IEnumerator WaitAndMoveToNextPoint()
    {
        Stop();
        yield return new WaitForSeconds(5f);
        //GotoNextPoint();
        Move();
    }

    void Pursue()
    {
        TargetPlayer();

        // Stop and attack when in range.
        if(agent.remainingDistance > attackRange)
        {
            Move();
        }
        else
        {
            Stop();
            LookAtPlayer();
            Attack();
        }
    }

    void Search()
    {
        if(currentSearchTime > 0f)
        {
            // Count down search timer.
            currentSearchTime -= Time.deltaTime;
        }
        else
        {
            // Patrol and reset clock.
            currentSearchTime = searchTime;
        }
    }

    void Attack()
    {
        Debug.Log("EG Attacks " + Time.deltaTime);
    }

    void LookAtPlayer()
    {
        transform.LookAt(playerLastPosition);
    }

    void TargetPlayer()
    {
        agent.destination = playerLastPosition;
    }

    void Move()
    {
        agent.isStopped = false;
    }

    void Stop()
    {
        agent.isStopped = true;
    }
}