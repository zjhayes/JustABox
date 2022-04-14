using UnityEngine;

[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(EnemyAlertController))]
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject player;

    private EventManager events;
    private EnemyAlertController alertController;

    void Start()
    {
        events = GetComponent<EventManager>();
        alertController = GetComponent<EnemyAlertController>();
    }

    public GameObject Player
    {
        get { return player; }
    }

    public EventManager Events
    {
        get { return events; }
    }
}
