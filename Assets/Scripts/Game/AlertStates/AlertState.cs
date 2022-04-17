using UnityEngine;

public class AlertState : MonoBehaviour, IState<AlertController>
{
    private AlertController controller;
    
    public void Handle(AlertController _controller)
    {
        controller = _controller;
    }

    void Update()
    {
        // Iterate alert timer.
        if(controller.CurrentTime > 0.0f)
        {
            if(Time.frameCount % 60 == 0)
            {
                Debug.Log("Alert Time: " + controller.CurrentTime);
            }
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