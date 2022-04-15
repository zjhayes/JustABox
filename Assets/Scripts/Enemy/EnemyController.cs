using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IController
{
    [SerializeField]
    private PatrolPath patrolPath;

    private Awareness awareness;
    private NavMeshAgent agent;
    private StateContext<EnemyController> stateContext;
    private bool reported;

    void Start()
    {
        awareness = GetComponent<Awareness>();
        agent = GetComponent<NavMeshAgent>();
        stateContext = new StateContext<EnemyController>(this);
        reported = false;

         agent.autoBraking = false;

        Patrol();
    }

    public void Patrol()
    {
        reported = false;
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
        reported = true;
    }

    public void Attack()
    {
        stateContext.Transition<AttackState>();
    }

    public void LookAtPlayer()
    {
        transform.LookAt(awareness.PlayerLastPosition);
    }

    public void TargetPlayer()
    {
        agent.destination = awareness.PlayerLastPosition.position;
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
    public bool Reported { get { return reported; }}
}