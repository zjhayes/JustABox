using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    private NavigationPoint[] navigationPoints;

    void Start()
    {
        navigationPoints = GetComponentsInChildren<NavigationPoint>();

        if(navigationPoints.Length == 0)
        {
            Debug.Log("No patrol route set. " + gameObject.name);
        }
    }

    public NavigationPoint[] NavigationPoints 
    {
        get
        {
            return navigationPoints;
        }
    }
}
