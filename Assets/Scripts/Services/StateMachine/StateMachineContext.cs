using HierarchicalStateMachine;
using System.Collections.Generic;
using System;

public abstract class StateMachineContext<TState, TStateEnum> : IStateMachine<TState>
    where TState : BaseState<TState>
    where TStateEnum : Enum
{
    private Dictionary<TStateEnum, TState> states;
    private TState currentState;

    protected StateMachineContext(Dictionary<TStateEnum, TState> initialStateDictionary, TStateEnum defaultState)
    {
        states = initialStateDictionary;
        CurrentState = GetState(defaultState);
    }

    public TState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public TState GetState(TStateEnum stateEnum)
    {
        if (TryGetState(stateEnum, out TState state))
        {
            return state;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public bool TryGetState(TStateEnum stateEnum, out TState state)
    {
        return states.TryGetValue(stateEnum, out state);
    }

    public void TransitionTo(TStateEnum nextStateEnum)
    {
        if (TryGetState(nextStateEnum, out TState nextState))
        {
            SwitchState(nextState);
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    private void SwitchState(TState newState)
    {
        CurrentState.Exit(); // Exit current state.
        if (CurrentState.SuperState == null)
        {
            CurrentState = newState;
        }
        else
        {
            CurrentState.SuperState.SetSubState(newState);
        }
        newState.Enter();
    }
}