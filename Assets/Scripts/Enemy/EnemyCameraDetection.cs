using UnityEngine;

public class EnemyCameraDetection : GameBehaviour
{
    [SerializeField]
    private Camera surveillanceCamera;
    [SerializeField]
    private float detectionRange = 20f;
    [SerializeField]
    private LayerMask playerLayer;

    private bool isPlayerDetected = false;
    private Transform playerTransform;

    public event Events.EnemyEvent OnPlayerDetected;
    public event Events.EnemyEvent OnPlayerLost;

    private void Start()
    {
        playerTransform = gameManager.Player.transform;
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange, playerLayer))
        {
            if (hit.transform == playerTransform)
            {
                if (!isPlayerDetected)
                {
                    isPlayerDetected = true;
                    PlayerDetected();
                }
            }
        }
        else
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
                PlayerLost();
            }
        }
    }

    private void PlayerDetected()
    {
        OnPlayerDetected.Invoke();
    }

    private void PlayerLost()
    {
        OnPlayerLost.Invoke();
    }

    public bool IsPlayerDetected()
    {
        return isPlayerDetected;
    }
}