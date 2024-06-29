using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private InputManager input;
    [SerializeField]
    private EventManager events;
    [SerializeField]
    private EnemyAlertController alertController;

    void Awake()
    {
        // Inject gameManager into dependents.
        ServiceInjector.Resolve<IGameManager, GameBehaviour>(this);
    }

    public GameObject Player
    {
        get { return player; }
    }

    public EventManager Events
    {
        get { return events; }
    }

    public InputManager Input
    {
        get { return input; }
    }

    public CameraController Camera
    {
        get { return cameraController; }
    }

    public EnemyAlertController EnemyAlert
    {
        get { return alertController; }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
