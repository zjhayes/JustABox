using UnityEngine;

[RequireComponent(typeof(EventManager))]
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject player;

    private EventManager events;

    void Start()
    {
        events = GetComponent<EventManager>();
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
