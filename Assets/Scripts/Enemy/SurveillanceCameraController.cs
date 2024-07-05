using UnityEngine;

public class SurveillanceCameraController : GameBehaviour
{
    [SerializeField]
    private Camera surveillanceCamera;
    [SerializeField]
    private GameObject cameraBody;

    [SerializeField]
    private float rotationSpeed = 1f; // Speed of the back and forth rotation
    [SerializeField]
    private float rotationAngle = 45f; // Maximum angle for rotation
    [SerializeField]
    private float pauseDuration = 1f; // Duration to pause at each end
    [SerializeField]
    private int priority = 0;


    private float rotationTime = 0f;
    private float pauseTime = 0f;
    private bool isPausing = false;
    private bool isRotatingForward = true;

    private void Awake()
    {
        gameManager.UI.SurveillanceView.AddCamera(this, priority);
    }

    private void Update()
    {
        RotateCameraBody();
    }

    private void RotateCameraBody()
    {
        if (isPausing)
        {
            pauseTime += Time.deltaTime;
            if (pauseTime >= pauseDuration)
            {
                pauseTime = 0f;
                isPausing = false;
                isRotatingForward = !isRotatingForward;
            }
            return;
        }

        float angle = Mathf.Sin(rotationTime * rotationSpeed) * rotationAngle;

        if ((isRotatingForward && angle >= rotationAngle - 0.1f) || (!isRotatingForward && angle <= -rotationAngle + 0.1f))
        {
            isPausing = true;
        }

        cameraBody.transform.localRotation = Quaternion.Euler(cameraBody.transform.localRotation.x, angle, cameraBody.transform.localRotation.y);

        rotationTime += Time.deltaTime;
    }


    public Camera SurveillanceCamera
    {
        get { return surveillanceCamera; }
    }
}
