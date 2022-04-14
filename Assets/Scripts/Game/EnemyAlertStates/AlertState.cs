using UnityEngine;

public class AlertState : MonoBehaviour, IState<EnemyAlertController>
{
    [SerializeField]
    private float alertTime = 99.0f;
    private float currentTime;
    private EnemyAlertController controller;
    
    public void Handle(EnemyAlertController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        currentTime = alertTime;
    }

    void Update()
    {
        // Iterate alert timer.
        if(currentTime > 0)
        {
            Debug.Log("Alert Time: " + currentTime);
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = alertTime;
            controller.AllClear();
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<AlertState>();
        Destroy(comp);
    }
}