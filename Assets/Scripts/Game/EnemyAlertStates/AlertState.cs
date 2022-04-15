using UnityEngine;

public class AlertState : MonoBehaviour, IState<EnemyAlertController>
{
    private EnemyAlertController controller;
    
    public void Handle(EnemyAlertController _controller)
    {
        controller = _controller;
    }

    void Update()
    {
        // Iterate alert timer.
        if(controller.CurrentTime > 0.0f)
        {
            Debug.Log("Alert Time: " + controller.CurrentTime);
            controller.CurrentTime -= Time.deltaTime;
        }
        else
        {
            controller.AllClear();
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<AlertState>();
        Destroy(comp);
    }
}