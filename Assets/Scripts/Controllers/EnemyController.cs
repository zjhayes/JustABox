using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(Awareness))]
public class EnemyController : MonoBehaviour, IController
{
    [SerializeField]
    private PatrolPath patrolPath;
    private Awareness awareness;
    private NavMeshAgent agent;
    private StateContext<EnemyController> stateContext;

    void Start()
    {
        awareness = GetComponent<Awareness>();
        agent = GetComponent<NavMeshAgent>();
        stateContext = new StateContext<EnemyController>(this);

        Patrol();
    }

    public void Patrol()
    {
        stateContext.Transition<PatrolState>();
    }

    public void Alert()
    {
        stateContext.Transition<AlertState>();
    }

    public void Search()
    {
        stateContext.Transition<SearchState>();
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
}
