using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : GameBehaviour, IController
{
    [SerializeField]
    private PatrolPath patrolPath;

    private Awareness awareness;
    private NavMeshAgent agent;
    private StateContext<EnemyController> stateContext;
    private bool alertReported = false;

    void Start()
    {
        awareness = GetComponent<Awareness>();
        agent = GetComponent<NavMeshAgent>();
        stateContext = new StateContext<EnemyController>(this);
        
        agent.autoBraking = false;
        alertReported = false;

        Patrol();
    }

    void OnEnable()
    {
        EventBus.Subscribe(EventType.OnAlert, OnAlert);
        EventBus.Subscribe(EventType.OnAllClear, OnAllClear);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnAlert, OnAlert);
        EventBus.Unsubscribe(EventType.OnAllClear, OnAllClear);
    }

    private void OnAlert()
    {
        alertReported = true;
        Pursue();
    }

    private void OnAllClear()
    {
        Patrol();
    }

    public void Patrol()
    {
        alertReported = false;
        stateContext.Transition<PatrolState>();
    }

    public void Pursue()
    {
        stateContext.Transition<PursueState>();
    }

    public void Search()
    {
        stateContext.Transition<SearchState>();
    }

    public void ReportAlert()
    {
        stateContext.Transition<ReportState>();
        alertReported = true;
    }

    public void Attack()
    {
        stateContext.Transition<AttackState>();
    }

    public void LookAtPlayer()
    {
        transform.LookAt(awareness.PlayerLastPosition);
    }

    public void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }

    public void Move()
    {
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public NavMeshAgent Agent { get { return agent; } }
    public Awareness Awareness { get { return awareness; } }
    public PatrolPath PatrolPath { get { return patrolPath; } }
    public bool AlertReported { get { return alertReported; }}

    public Vector3 PlayerLastKnownPosition
    {
        get
        {
            if(alertReported)
            {
                // Return search area during alert.
                return gameManager.EnemyAlert.SearchArea;
            }
            else
            {
                return awareness.PlayerLastPosition;
            }
        }
    }
}