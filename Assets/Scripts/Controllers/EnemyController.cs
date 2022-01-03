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
    private StateContext<EnemyController> stateContext;

    void Start()
    {
        awareness = GetComponent<Awareness>();

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

    public Awareness Awareness { get; }
    public PatrolPath PatrolPath { get; set; }
}
