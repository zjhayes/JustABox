using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertController : MonoBehaviour, IController
{
    private StateContext<EnemyAlertController> stateContext;

    void Start()
    {
        stateContext = new StateContext<EnemyAlertController>(this);
        stateContext.Transition<NormalState>();
    }

    public void AllClear()
    {
        stateContext.Transition<NormalState>();
        GameManager.Instance.Events.AllClear();
    }

    public void Alert()
    {
        stateContext.Transition<AlertState>();
        GameManager.Instance.Events.Alert();
    }

    public void Evasion()
    {
        //stateContext.Transition<EvasionState>();
        
    }
}
