using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReportState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;
    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;

    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if(!controller) { Debug.Log("No controller set on state."); }
    }

    void Update()
    {
        // Report to alert controller.
        WaitAndUpdateGlobalAlert();
    }

    IEnumerator WaitAndUpdateGlobalAlert()
    {
        controller.Stop();
        yield return new WaitForSeconds(5f);
        // Set global alert.
        GameManager.Instance.EnemyAlertController.Alert();
        // Resume pursuit.
        controller.Move();
        controller.Pursue();
    }

    public void Destroy()
    {
        var comp = GetComponent<ReportState>();
        Destroy(comp);
    }
}