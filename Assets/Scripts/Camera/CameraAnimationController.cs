using UnityEngine;

public class CameraAnimationController : GameBehaviour
{
    [SerializeField]
    private Animator cameraStateAnimator; // Controls state driven camera.

    public const string TPC_STATE = "ThirdPersonCamera";
    public const string FPC_STATE = "FirstPersonCamera";

    private void Start()
    {
        gameManager.Camera.OnToggleView += PlayToggleView;    
    }

    private void PlayToggleView()
    {
        if (gameManager.Camera.IsFirstPerson)
        {
            cameraStateAnimator.Play(FPC_STATE);
        }
        else
        {
            cameraStateAnimator.Play(TPC_STATE);
        }
    }
}
