using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertController : MonoBehaviour, IController
{
    [SerializeField]
    private float alertTime = 9.9f;
    private float currentTime;

    private StateContext<EnemyAlertController> stateContext;

    void Start()
    {
        stateContext = new StateContext<EnemyAlertController>(this);
        stateContext.Transition<NormalState>();
        currentTime = alertTime;
    }

    public void AllClear()
    {
        Reset();
        stateContext.Transition<NormalState>();
        GameManager.Instance.Events.AllClear();
    }

    public void Alert()
    {
        stateContext.Transition<AlertState>();
        GameManager.Instance.Events.Alert();
    }

    public void Reset()
    {
        currentTime = alertTime;
        GameManager.Instance.Events.ResetAlert();
    }

    public float AlertTime
    {
        get { return alertTime; }
    }

    public float CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }
    public void Evasion()
    {
        //stateContext.Transition<EvasionState>();
        
    }
}
