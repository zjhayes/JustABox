using UnityEngine;

public class EnemyAlertController : GameBehaviour, IController
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
        gameManager.Events.AllClear();
        Debug.Log("All Clear");
    }

    public void Alert(Vector3 playerLastPosition)
    {
        searchArea = playerLastPosition;
        stateContext.Transition<AlertState>();
        gameManager.Events.Alert();
        Debug.Log("Alert Reported");
    }

    public void Reset(Vector3 playerLastPosition)
    {
        searchArea = playerLastPosition;
        currentTime = alertTime;
        gameManager.Events.ResetAlert();
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
