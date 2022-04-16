using UnityEngine;

public class SearchState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    
    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        // Set search area.
        Debug.Log("Searching");
        // TODO: Add search system.
    }

    public void Destroy()
    {
        var comp = GetComponent<SearchState>();
        Destroy(comp);
    }
}
