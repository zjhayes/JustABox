using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private SurveillanceViewController surveillanceViewController;

    public SurveillanceViewController SurveillanceView
    {
        get { return surveillanceViewController; }
    }
}
