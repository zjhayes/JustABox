using UnityEngine;

public class NormalState : MonoBehaviour, IState<AlertController>
{
    private AlertController controller;
    
    public void Handle(AlertController _controller)
    {
        controller = _controller;
    }

    public void Destroy()
    {
        var comp = GetComponent<NormalState>();
        Destroy(comp);
    }
}