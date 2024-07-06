using HierarchicalStateMachine;

public class SurveillanceState : BaseState<SurveillanceState>
{
    protected SurveillanceCameraController controller;
    protected IGameManager gameManager;

    protected SurveillanceState(SurveillanceCameraController controller, IGameManager gameManager) : base()
    {
        this.controller = controller;
        this.gameManager = gameManager;
    }

    protected override void InitializeSubState(){}
}
