using HierarchicalStateMachine;
using System.Collections.Generic;

public class EnemyContext : IStateMachine
{
    private Dictionary<EnemyStates, EnemyState> states;
    private BaseState currentState;

    public EnemyContext(EnemyController controller, IGameManager gameManager, EnemyStates defaultState = EnemyStates.PATROL)
    {
        // Setup enemy states.
        states = new Dictionary<EnemyStates, EnemyState>
        {
            { EnemyStates.PATROL, new PatrolState(controller, gameManager) }
        };

        // Set default state.
        CurrentState = GetState(defaultState);
    }

    public BaseState CurrentState 
    { 
        get { return currentState; }
        set { currentState = value; }
    }

    public EnemyState GetState(EnemyStates stateEnum)
    {
        if (TryGetState(stateEnum, out EnemyState state))
        {
            return state;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public bool TryGetState(EnemyStates stateEnum, out EnemyState state)
    {
        return states.TryGetValue(stateEnum, out state);
    }

    public void TransitionTo(EnemyStates nextStateEnum)
    {
        if (TryGetState(nextStateEnum, out EnemyState nextState))
        {
            CurrentState.SwitchState(nextState);
        }
        else
        {
            throw new StateNotFoundException();
        }
    }
}