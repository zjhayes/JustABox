using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CameraController : GameBehaviour
{
    [SerializeField]
    private Animator cameraStateAnimator; // Controls state driven camera.

    const string TPC_STATE = "ThirdPersonCamera";
    const string FPC_STATE = "FirstPersonCamera";
    bool firstPerson = false;

    void Start()
    {
       gameManager.Input.Controls.Player.Camera.performed += _ => SwitchView();
    }

    void SwitchView()
    {
        firstPerson = !firstPerson;

        if(firstPerson)
        {
            cameraStateAnimator.Play(FPC_STATE);
        }
        else
        {
            cameraStateAnimator.Play(TPC_STATE);
        }
    }
}
