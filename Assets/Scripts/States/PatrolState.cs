using System.Collections;
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
        Debug.Log("Patrolling");
        controller = _controller;
    }

    void Start()
    {
        agent = controller.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE)
        {
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
        agent.destination = path.NavigationPoints[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % path.NavigationPoints.Length;
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
