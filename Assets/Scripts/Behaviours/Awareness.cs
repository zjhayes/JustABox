using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : MonoBehaviour
{
    [SerializeField]
    private float awareDistance = 10.0f;

    private Transform player;
    private Vector3 playerLastPosition;

    readonly float SIGHT_OFFSET = -0.5f;
    readonly string PLAYER_TAG = "Player";
    readonly float NUMBER_OF_RAYS = 24.0f;

    void Start()
    {
        player = GameManager.Instance.Player.transform;
    }
    
    public bool CanSeePlayer(float sightOffset, float startAngleOffset, float stepAngleOffset)
    {
        Quaternion startingAngle = Quaternion.AngleAxis(startAngleOffset, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(stepAngleOffset, Vector3.up);
        Quaternion angle = transform.rotation * startingAngle;
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += sightOffset;
        Vector3 forward = angle * Vector3.forward;
        for(int i = 0; i < NUMBER_OF_RAYS; i++)
        {
            if(Physics.Raycast(position, forward, out hit, awareDistance))
            {
                if(hit.collider.tag == PLAYER_TAG)
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    playerLastPosition = player.transform.position;
                    Debug.Log("Player spotted.");
                    return true;
                }
            }
            else
            {
                Debug.DrawRay(position, forward * awareDistance, Color.white);
            }
            forward = stepAngle * forward;
        }
        return false;
    }

    
}