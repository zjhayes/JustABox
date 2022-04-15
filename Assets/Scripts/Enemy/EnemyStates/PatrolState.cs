using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    private int destinationPoint = 0;
    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;

    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if(!controller) { Debug.Log("No controller set on state."); }
    }

    void Update()
    {
        // Check awareness.
        if(controller.Awareness.CanSeePlayer())
        {
            controller.Pursue();

        } // When near destination, wait and move to new destination.
        else if(NearDestination() && !controller.Agent.isStopped)
        {
            StartCoroutine(WaitAndMoveToNextPoint());
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<PatrolState>();
        Destroy(comp);
    }

    private IEnumerator WaitAndMoveToNextPoint()
    {
        controller.Stop();
        yield return new WaitForSeconds(5f);
        GotoNextPoint();
        controller.Move();
    }

    private void GotoNextPoint()
    {
        // Set destination to next patrol route point.
        controller.Agent.destination = controller.PatrolPath.NavigationPoints[destinationPoint].transform.position;
        destinationPoint = (destinationPoint + 1) % controller.PatrolPath.NavigationPoints.Length;
    }

    private bool NearDestination()
    {
        return controller.Agent.remainingDistance < PATROL_POINT_MIN_DISTANCE;
    }
}