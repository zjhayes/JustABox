using UnityEngine;

public class SearchState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    
    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    public void Destroy()
    {
        var comp = GetComponent<SearchState>();
        Destroy(comp);
    }
}
