using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReportState : MonoBehaviour, IState<EnemyController>
{
    private EnemyController controller;

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
        // Report to alert controller. TODO: Replace with report animation.
        StartCoroutine(WaitAndUpdateGlobalAlert());
    }

    private IEnumerator WaitAndUpdateGlobalAlert()
    {
        controller.Stop();
        Debug.Log("Reporting...");
        yield return new WaitForSeconds(5f);
        Debug.Log("Reported.");
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