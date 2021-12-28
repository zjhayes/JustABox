using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : CharacterAnimator
{
    PlayerMovement playerMovement;

    protected override void Start()
    {
        base.Start();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public override float MaxSpeed
    {
        // Use running speed as maximum speed.
        get { return playerMovement.RunningSpeed; }
    }
}
