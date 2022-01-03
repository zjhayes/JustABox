using System.Collections.Generic;
using UnityEngine;
public class StateContext<T> where T : IController
{
    public IState<T> CurrentState
    {
        get; set;
    }

    private readonly T controller;

    public StateContext(T _controller)
    {
        controller = _controller;
    }

    public void Transition()
    {
        CurrentState.Handle(controller);
    }

    public void Transition<State>() where State : IState
    {
        if(CurrentState != null)
        {
            CurrentState.Destroy();
        }
        //var comp = typeof(IState<T>);
        CurrentState = controller.gameObject.AddComponent<State>();
        //CurrentState = state;
        CurrentState.Handle(controller);
    }
}
