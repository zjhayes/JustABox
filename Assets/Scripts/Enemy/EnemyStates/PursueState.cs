using UnityEngine;

public class PursueState : MonoBehaviour, IState<EnemyController>
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
        // Check awareness.
        if(controller.Awareness.CanSeePlayer())
        {
            // Update Alert
        }
    }

    public void Destroy()
    {
        var comp = GetComponent<PursueState>();
        Destroy(comp);
    }
}
