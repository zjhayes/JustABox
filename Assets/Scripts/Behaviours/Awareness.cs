using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : MonoBehaviour
{
    [SerializeField]
    private float awareDistance = 25.0f;
    [SerializeField]
    private float sightOffset = 0;
    [SerializeField]
    private int numberOfSightRays = 24;
    [SerializeField]
    private int sightStartAngle = -60;
    [SerializeField]
    private int sightStepAngle = 5;
    [SerializeField]
    private int scanInterval = 10;

    readonly string PLAYER_TAG = "Player";

    private Vector3 playerLastPosition;
    private bool canSeePlayer = false;

    void Update()
    {
        // Scan for detectable every x (scanInterval) frames.
        if(Time.frameCount % scanInterval == 0)
        {
            ScanSight();
        }
    }

    private void ScanSight()
    {
        Scan(sightOffset, sightStartAngle, sightStepAngle);
    }
    
    private void Scan(float scanOffset, float startAngleOffset, float stepAngleOffset)
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
                if(hit.collider.GetComponent<Detectable>())
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    hit.collider.GetComponent<Detectable>().Action(GetComponent<IController>());
                }
            }
            else
            {
                Debug.DrawRay(position, forward * awareDistance, Color.white);
            }
            forward = stepAngle * forward;
        }
    }

    public Vector3 PlayerLastPosition { get { return playerLastPosition; } }
    public bool CanSeePlayer { get { return canSeePlayer; }}
}