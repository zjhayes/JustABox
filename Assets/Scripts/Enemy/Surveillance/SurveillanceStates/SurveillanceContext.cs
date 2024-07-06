using System.Collections.Generic;

public class SurveillanceContext : StateMachineContext<SurveillanceState, SurveillanceStates>
{
    public SurveillanceContext(SurveillanceCameraController controller, IGameManager gameManager, SurveillanceStates defaultState = SurveillanceStates.LOOK_AHEAD)
        : base(new Dictionary<SurveillanceStates, SurveillanceState>
        {
            { SurveillanceStates.LOOK_AHEAD, new LookAheadState(controller, gameManager) },
            { SurveillanceStates.PAN, new PanState(controller, gameManager) },
            { SurveillanceStates.FOLLOW_TARGET, new FollowTargetState(controller, gameManager) }
        }, defaultState)
    {
    }
}
