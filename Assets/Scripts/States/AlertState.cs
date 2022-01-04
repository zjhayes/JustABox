using UnityEngine;

public class AlertState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    
    public void Handle(EnemyController _controller)
    {
        controller = _controller;
        Debug.Log("Player spotted.");
    }

    public void Destroy()
    {
        var comp = GetComponent<AlertState>();
        Destroy(comp);
    }
}
