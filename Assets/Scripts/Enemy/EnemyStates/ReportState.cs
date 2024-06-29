using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReportState : GameBehaviour, IState<EnemyController>
{
    private EnemyController controller;

    public void Handle(EnemyController _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if(!controller) { Debug.Log("No controller set on state."); }
        Debug.Log("Reporting...");
    }

    void Update()
    {
        // Report to alert controller. TODO: Replace with report animation.
        StartCoroutine(WaitAndUpdateGlobalAlert());
    }

    private IEnumerator WaitAndUpdateGlobalAlert()
    {
        controller.Stop();
        yield return new WaitForSeconds(5f);
        Debug.Log("Reported.");
        // Set global alert.
        gameManager.EnemyAlert.Alert(controller.Awareness.PlayerLastPosition);
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