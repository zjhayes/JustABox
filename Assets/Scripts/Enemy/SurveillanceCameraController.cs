using UnityEngine;

public class SurveillanceCameraController : GameBehaviour
{
    [SerializeField]
    private Camera surveillanceCamera;

    private void Awake()
    {
        gameManager.UI.SurveillanceView.AddCamera(this);
    }

    public Camera SurveillanceCamera
    {
        get { return surveillanceCamera; }
    }
}
