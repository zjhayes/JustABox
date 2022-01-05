using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : MonoBehaviour
{
    [SerializeField]
    private float awareDistance = 10.0f;
    [SerializeField]
    private float sightOffset = 0;
    [SerializeField]
    private int numberOfSightRays = 24;
    [SerializeField]
    private int sightStartAngle = -60;
    [SerializeField]
    private int sightStepAngle = 5;

    readonly string PLAYER_TAG = "Player";

    private Transform player;
    private Transform playerLastPosition;

    void Start()
    {
        player = GameManager.Instance.Player.transform;
    }

    public bool CanSeePlayer()
    {
        List<Transform> playerInView = Scan(PLAYER_TAG, sightOffset, sightStartAngle, sightStepAngle);
        if(playerInView.Count > 0)
        {
            playerLastPosition = playerInView[0];
            return true;
        }
        return false;
    }
    
    private List<Transform> Scan(string tag, float scanOffset, float startAngleOffset, float stepAngleOffset)
    {
        Quaternion startingAngle = Quaternion.AngleAxis(startAngleOffset, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(stepAngleOffset, Vector3.up);
        Quaternion angle = transform.rotation * startingAngle;
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += scanOffset;
        Vector3 forward = angle * Vector3.forward;
        List<Transform> hits = new List<Transform>();
        for(int i = 0; i < numberOfSightRays; i++)
        {
            if(Physics.Raycast(position, forward, out hit, awareDistance))
            {
                if(hit.collider.tag == tag)
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    hits.Add(hit.collider.transform);
                }
            }
            else
            {
                Debug.DrawRay(position, forward * awareDistance, Color.white);
            }
            forward = stepAngle * forward;
        }
        return hits;
    }

    public Transform PlayerLastPosition { get { return playerLastPosition; } }
}