using UnityEngine;

[RequireComponent(typeof(EnemyCameraDetection))]
public class SurveillanceCameraController : GameBehaviour
{
    [SerializeField]
    private Camera surveillanceCamera;
    [SerializeField]
    private GameObject cameraBody;
    [SerializeField]
    EnemyCameraDetection cameraDetection;
    [SerializeField]
    private float zoomMultiplier = 2.0f;
    [SerializeField]
    private float rotationSpeed = 1f; // Speed of the back and forth rotation
    [SerializeField]
    private float rotationAngle = 45f; // Maximum angle for rotation
    [SerializeField]
    private float pauseDuration = 1f; // Duration to pause at each end
    [SerializeField]
    private int priority = 0;
    [SerializeField]
    private SurveillanceStates defaultState = SurveillanceStates.LOOK_AHEAD;

    private SurveillanceCameraSettings defaultCameraSettings;
    private SurveillanceContext context;

    public SurveillanceContext Context
    {
        get { return context; }
    }

    private void Awake()
    {
        context = new SurveillanceContext(this, gameManager, defaultState);

        gameManager.UI.SurveillanceView.AddCamera(this, priority);
        cameraDetection.OnPlayerDetected += OnPlayerDetected;
        cameraDetection.OnPlayerLost += OnPlayerLost;
        defaultCameraSettings = new SurveillanceCameraSettings(cameraBody.transform, surveillanceCamera.fieldOfView);
    }

    private void Update()
    {
        context.CurrentState.Update();
    }

    private void OnPlayerDetected()
    {
        context.TransitionTo(SurveillanceStates.FOLLOW_TARGET);
    }

    private void OnPlayerLost()
    {
        context.TransitionTo(defaultState);
    }

    public Camera SurveillanceCamera
    {
        get { return surveillanceCamera; }
    }

    public GameObject CameraBody
    {
        get { return cameraBody; }
    }

    public float ZoomMultiplier
    {
        get { return zoomMultiplier; }
    }

    public float RotationSpeed
    {
        get { return rotationSpeed; }
    }

    public float RotationAngle
    {
        get { return rotationAngle; }
    }

    public float PauseDuration
    {
        get { return pauseDuration; }
    }

    public SurveillanceCameraSettings DefaultCameraSettings
    {
        get { return defaultCameraSettings; }
    }

    public void Reset()
    {
        surveillanceCamera.fieldOfView = defaultCameraSettings.FieldOfView;
        cameraBody.transform.position = defaultCameraSettings.Transform.position;
        cameraBody.transform.rotation = defaultCameraSettings.Transform.rotation;
    }
}
