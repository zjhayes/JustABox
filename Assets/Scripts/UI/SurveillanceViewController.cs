using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveillanceViewController : GameBehaviour
{
    [SerializeField]
    private RawImage displayImage;
    [SerializeField]
    private RenderTexture surveillanceViewTexture;

    private List<SurveillanceCameraController> surveillanceCameras = new List<SurveillanceCameraController>();
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Assign input controls to surveillance view.
        gameManager.Input.Controls.Surveillance.NextCamera.canceled += _ => NextCamera();
        gameManager.Input.Controls.Surveillance.LastCamera.canceled += _ => LastCamera();

        EnableCurrentCamera();
    }

    public void AddCamera(SurveillanceCameraController surveillanceCamera, int? priority = null)
    {
        if (priority.HasValue)
        {
            int index = Mathf.Clamp(priority.Value, 0, surveillanceCameras.Count);
            surveillanceCameras.Insert(index, surveillanceCamera);
        }
        else
        {
            surveillanceCameras.Add(surveillanceCamera);
        }
    }

    private void NextCamera()
    {
        DisableCurrentCamera();

        currentCameraIndex = (currentCameraIndex + 1) % surveillanceCameras.Count;

        EnableCurrentCamera();
    }

    private void LastCamera()
    {
        DisableCurrentCamera();

        currentCameraIndex = (currentCameraIndex - 1 + surveillanceCameras.Count) % surveillanceCameras.Count;

        EnableCurrentCamera();
    }

    private void EnableCurrentCamera()
    {
        surveillanceCameras[currentCameraIndex].enabled = true;
        surveillanceCameras[currentCameraIndex].SurveillanceCamera.targetTexture = surveillanceViewTexture;
    }

    private void DisableCurrentCamera()
    {
        surveillanceCameras[currentCameraIndex].SurveillanceCamera.targetTexture = null;
        surveillanceCameras[currentCameraIndex].enabled = false;
    }
}
