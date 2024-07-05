
public class CameraController : GameBehaviour
{
    private bool firstPerson = false;

    public event Events.CameraEvent OnToggleView;

    private void Start()
    {
       gameManager.Input.Controls.Camera.ToggleView.performed += _ => ToggleView();
    }

    private void ToggleView()
    {
        firstPerson = !firstPerson;
        OnToggleView?.Invoke();
    }

    public bool IsFirstPerson
    {
        get { return firstPerson; }
    }
}
