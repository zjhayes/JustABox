using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(EnemyAwareness))]
public class EnemyController : MonoBehaviour, IController
{
    private EnemyAwareness awareness;
    private System.Type /*IState<EnemyController>*/ PATROL, ALERT, SEARCH;
    private StateContext<EnemyController> stateContext;

    void Start()
    {
        awareness = GetComponent<EnemyAwareness>();

        stateContext = new StateContext<EnemyController>(this);
        PATROL = typeof(PatrolState);//gameObject.AddComponent<PatrolState>();
        //ALERT = gameObject.AddComponent<AlertState>();
        //SEARCH = gameObject.AddComponent<SearchState>();

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

    public EnemyAwareness Awareness { get; }
}
