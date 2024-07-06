using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    public Transform FollowTarget
    {
        get { return followTarget; }
    }
}
