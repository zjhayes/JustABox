using UnityEngine;

public class AlertState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    
    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        controller.TargetPlayer();
        controller.Move();
    }

    void Update()
    {
        if(controller.Awareness.CanSeePlayer())
        {
            controller.TargetPlayer();
            controller.LookAtPlayer();
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<AlertState>();
        Destroy(comp);
    }
}
