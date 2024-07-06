
public class LookAheadState : SurveillanceState
{
    public LookAheadState(SurveillanceCameraController controller, IGameManager gameManager) : base(controller, gameManager) { }

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
        base.Update();
    }
}

