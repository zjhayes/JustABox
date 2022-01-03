using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    GameObject player;

    public GameObject Player
    {
        get { return player; }
    }
}
