using UnityEngine;

[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(AlertController))]
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject player;

    private EventManager events;
    private AlertController alertController;

    void Start()
    {
        events = GetComponent<EventManager>();
        alertController = GetComponent<AlertController>();
    }

    public GameObject Player
    {
        get { return player; }
    }

    public EventManager Events
    {
        get { return events; }
    }

    public AlertController AlertController
    {
        get { return alertController; }
    }
}
