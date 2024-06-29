using UnityEngine;

public interface IGameManager : IService
{

    public GameObject Player { get; }

    public EventManager Events { get; }

    public InputManager Input { get; }
    public UIManager UI { get; }

    public CameraController Camera { get; }

    public EnemyAlertController EnemyAlert { get; }

    public void Quit();
}
