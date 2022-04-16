using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertController : MonoBehaviour, IController
{
    [SerializeField]
    private float alertTime = 9.9f;
    private float currentTime;
    private Vector3 searchArea;

    private StateContext<EnemyAlertController> stateContext;

    void Start()
    {
        stateContext = new StateContext<EnemyAlertController>(this);
        stateContext.Transition<NormalState>();
        currentTime = alertTime;
    }

    public void AllClear()
    {
        currentTime = alertTime;
        stateContext.Transition<NormalState>();
        GameManager.Instance.Events.AllClear();
        Debug.Log("All Clear");
    }

    public void Alert(Vector3 playerLastPosition)
    {
        searchArea = playerLastPosition;
        stateContext.Transition<AlertState>();
        GameManager.Instance.Events.Alert();
        Debug.Log("Alert Reported");
    }

    public void Reset(Vector3 playerLastPosition)
    {
        searchArea = playerLastPosition;
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

    public Vector3 SearchArea
    {
        get { return searchArea; }
        set { searchArea = value; }
    }
}
