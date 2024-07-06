using UnityEngine;

public class FollowTargetController : GameBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float sensitivity = 10f;
    [SerializeField]
    private float maxOffset = 2f; // Maximum distance the target can move from its original position

    private Vector3 originalPosition;
    private Vector2 initialMousePosition;

    private void Awake()
    {
        Cursor.visible = false;
        originalPosition = target.transform.localPosition;

        gameManager.Input.Controls.Camera.ControlFollowTarget.started += _ => Activate();
        gameManager.Input.Controls.Camera.ControlFollowTarget.canceled += _ => Deactivate();
        Deactivate();
    }

    private void Update()
    {
        Vector2 currentMousePosition = gameManager.Input.Controls.Camera.MousePosition.ReadValue<Vector2>();
        Vector2 mouseDelta = currentMousePosition - initialMousePosition;

        // Calculate the new position based on mouse input,
        Vector3 offset = new Vector3(mouseDelta.x, 0, mouseDelta.y) * sensitivity * Time.deltaTime;

        // Clamp the offset to the maximum allowed distance,
        Vector3 newPosition = target.transform.localPosition + offset;
        newPosition = originalPosition + Vector3.ClampMagnitude(newPosition - originalPosition, maxOffset);

        // Apply the new position to the target,
        target.transform.localPosition = newPosition;
    }

    private void Activate()
    {
        this.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        initialMousePosition = gameManager.Input.Controls.Camera.MousePosition.ReadValue<Vector2>();
    }

    private void Deactivate()
    {
        this.enabled = false;
        target.transform.localPosition = originalPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
