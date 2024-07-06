using UnityEngine;

public class SurveillanceCameraSettings
{
    private Transform transform;
    private float fieldOfView;

    public SurveillanceCameraSettings(Transform transform, float fieldOfView)
    {
        this.transform = transform;
        this.fieldOfView = fieldOfView;
    }

    public Transform Transform
    { 
        get { return transform; } 
    }

    public float FieldOfView
    { 
        get { return fieldOfView; } 
    }
}
