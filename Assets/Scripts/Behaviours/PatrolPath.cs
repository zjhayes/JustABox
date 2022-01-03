using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] navPoints;

    void Start()
    {
        if(navPoints.Length == 0)
        {
            Debug.Log("No patrol route set. " + gameObject.name);
        }
    }

    public Transform[] NavigationPoints { get; set; }
}
