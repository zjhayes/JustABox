using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;

    readonly int MAX_ATTACKS = 3;
    private float numberOfAttacks = 0;

    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if(!controller) { Debug.Log("No controller set on state."); }
        Debug.Log("Enemy attacks!");
    }

    void Update()
    {
        // Check awareness.
        if(controller.Awareness.CanSeePlayer)
        {
            controller.LookAtPlayer();

            if(controller.AlertReported)
            {
                // Reset alert.
                GameManager.Instance.AlertController.Reset(controller.Awareness.PlayerLastPosition);
            }
        }
        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        controller.Stop();

        // TODO: add attack system.
        yield return new WaitForSeconds(2f);

        if(controller.Awareness.CanSeePlayer && ++numberOfAttacks > MAX_ATTACKS) 
        {
            Attack();
        }
        else
        {
            // Resume pursuit.
            controller.Move();
            controller.Pursue();
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<AttackState>();
        Destroy(comp);
    }
}