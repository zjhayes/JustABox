using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls controls;

    override public void Awake() 
    {
        base.Awake();
        if(controls == null) { controls = new PlayerControls(); }
    }

    public PlayerControls Controls
    {
        get { return controls; }
    }

    void OnEnable()
    {
        // Turn controls on with this object.
        controls.Enable();
    }

    void OnDisable()
    {
        // Turn controls off with this object.
        controls.Disable();
    }
}