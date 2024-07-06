using UnityEngine;

public class PanState : SurveillanceState
{
    private bool isPausing = false;
    private bool isRotatingForward = true;
    private float rotationTime = 0f;
    private float pauseTime = 0f;

    public PanState(SurveillanceCameraController controller, IGameManager gameManager) : base(controller, gameManager) { }

    public override void Enter()
    {
        controller.Reset();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        RotateCameraBody();
        base.Update();
    }

    private void RotateCameraBody()
    {
        if (isPausing)
        {
            pauseTime += Time.deltaTime;
            if (pauseTime >= controller.PauseDuration)
            {
                pauseTime = 0f;
                isPausing = false;
                isRotatingForward = !isRotatingForward;
            }
            return;
        }

        float angle = Mathf.Sin(rotationTime * controller.RotationSpeed) * controller.RotationAngle;

        if ((isRotatingForward && angle >= controller.RotationAngle - 0.1f) || (!isRotatingForward && angle <= -controller.RotationAngle + 0.1f))
        {
            isPausing = true;
        }

        controller.CameraBody.transform.localRotation = Quaternion.Euler(controller.CameraBody.transform.localRotation.x, angle, controller.CameraBody.transform.localRotation.y);

        rotationTime += Time.deltaTime;
    }
}