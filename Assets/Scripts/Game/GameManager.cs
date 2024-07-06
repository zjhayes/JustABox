using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private InputManager input;
    [SerializeField]
    private UIManager ui;
    [SerializeField]
    private EventManager events;
    [SerializeField]
    private EnemyAlertController alertController;

    void Awake()
    {
        // Inject gameManager into dependents.
        ServiceInjector.Resolve<IGameManager, GameBehaviour>(this);
    }

    public PlayerManager Player
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

    public UIManager UI
    {
        get { return ui; }
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
