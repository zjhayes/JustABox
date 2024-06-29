using UnityEngine;

public class PursueState : GameBehaviour, IState<EnemyController>
{
    private EnemyController controller;

    readonly float PURSUIT_MIN_DISTANCE = 1.0f;
    readonly float ATTACK_MIN_DISTANCE = 10.0f;
    
    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        GoToPlayerLastKnownPosition();
    }

    void Update()
    {
        // Report alert, if none reported.
        if(!controller.AlertReported && !PlayerWithinAttackRange())
        {
            controller.ReportAlert();
        }

        if(controller.Awareness.CanSeePlayer)
        {
            if(controller.AlertReported) // there's an active alert...
            {
                // Reset active alerts.
                gameManager.EnemyAlert.Reset(controller.Awareness.PlayerLastPosition);
            }
            
            if(PlayerWithinAttackRange())
            {
                controller.Attack();
            }
            else
            {
                // Follow player.
                if(controller.Agent.isStopped)
                {
                    GoToPlayerLastKnownPosition();
                }
            }
        }
        else // can't see player...
        {
            // Pursue until reaching player's last known location.
            if(ReachedLastKnownLocation())
            {
                // Switch to Search State.
                controller.Search();
            }
        }
    }

    private void GoToPlayerLastKnownPosition()
    {
        controller.Move();
        controller.SetDestination(controller.PlayerLastKnownPosition);
    }

    private bool ReachedLastKnownLocation()
    {
        return controller.Agent.remainingDistance < PURSUIT_MIN_DISTANCE;
    }

    private bool PlayerWithinAttackRange()
    {
        return controller.Agent.remainingDistance < ATTACK_MIN_DISTANCE;
    }

    public void Destroy()
    {
        var comp = GetComponent<PursueState>();
        Destroy(comp);
    }
}
