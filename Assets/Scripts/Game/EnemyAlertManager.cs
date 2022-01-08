using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertManager : MonoBehaviour
{
    [SerializeField]
    private float alertTime = 99.0f;
    private float currentTime;

    void Awake()
    {
        currentTime = alertTime;
    }

    public void Alert()
    {
        GameManager.Events.Alert();
    }

    void IterateTimer()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = alertTime;
        }
        
    }
}
