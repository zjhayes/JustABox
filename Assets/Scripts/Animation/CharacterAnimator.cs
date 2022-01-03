using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    float smoothTime = .1f;

    NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null) { Debug.Log("Agent is missing from character animator component."); }
    }

    void Update()
    {
        // Set animation blend based on speed of character agent.
        float speedPercent = agent.velocity.magnitude / MaxSpeed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
    }

    public virtual float MaxSpeed
    {
        get { return agent.speed; }
    }
}
