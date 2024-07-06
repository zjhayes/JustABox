
public class FollowTargetState : SurveillanceState
{
    public FollowTargetState(SurveillanceCameraController controller, IGameManager gameManager) : base(controller, gameManager) { }

    public override void Enter()
    {
        Zoom();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        FollowPlayer();
        base.Update();
    }

    private void FollowPlayer()
    {
        controller.CameraBody.transform.LookAt(gameManager.Player.FollowTarget.transform.position);
    }

    private void Zoom()
    {
        controller.SurveillanceCamera.fieldOfView = controller.DefaultCameraSettings.FieldOfView / controller.ZoomMultiplier;
    }
}
