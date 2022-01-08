using UnityEngine;

public class NormalState : MonoBehaviour, IState<EnemyAlertController>
{
    private EnemyAlertController controller;
    
    public void Handle(EnemyAlertController _controller)
    {
        controller = _controller;
    }

    public void Destroy()
    {
        var comp = GetComponent<NormalState>();
        Destroy(comp);
    }
}