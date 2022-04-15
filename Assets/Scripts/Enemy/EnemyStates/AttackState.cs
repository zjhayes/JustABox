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
    }

    void Update()
    {
        // Check awareness.
        if(controller.Awareness.CanSeePlayer)
        {
            controller.LookAtPlayer();

            if(controller.Reported)
            {
                // Reset alert.
                GameManager.Instance.EnemyAlertController.Reset();
            }
        }
        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        controller.Stop();

        // TODO: add attack system.
        Debug.Log("Enemy attacks!");

        yield return new WaitForSeconds(5f);

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