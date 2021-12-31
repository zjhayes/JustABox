using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    Animator animator;
    const string TPC_STATE = "ThirdPersonCamera";
    const string FPC_STATE = "FirstPersonCamera";
    bool firstPerson = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        InputManager.Instance.Controls.Player.Camera.performed += _ => SwitchView();
    }

    void SwitchView()
    {
        firstPerson = !firstPerson;

        if(firstPerson)
        {
            animator.Play(FPC_STATE);
        }
        else
        {
            animator.Play(TPC_STATE);
        }
    }
}
