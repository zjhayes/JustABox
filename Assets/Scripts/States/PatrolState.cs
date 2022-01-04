using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    private int destinationPoint = 0;
    private NavMeshAgent agent;
    private PatrolPath path;
    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;

    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        agent = controller.GetComponent<NavMeshAgent>();
        path = controller.PatrolPath;
    }

    void Update()
    {
        if(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE && !agent.isStopped)
        {
            Debug.Log(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE);
            StartCoroutine(WaitAndMoveToNextPoint());
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<PatrolState>();
        Destroy(comp);
    }

    IEnumerator WaitAndMoveToNextPoint()
    {
        Stop();
        yield return new WaitForSeconds(5f);
        GotoNextPoint();
        Move();
    }

    void GotoNextPoint()
    {
        // Set destination to next patrol route point.
        agent.destination = controller.PatrolPath.NavigationPoints[destinationPoint].transform.position;
        destinationPoint = (destinationPoint + 1) % controller.PatrolPath.NavigationPoints.Length;
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
