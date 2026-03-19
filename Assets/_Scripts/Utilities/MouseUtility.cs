using UnityEngine;

public class MouseUtility
{
    private readonly Camera _camera;
    private readonly GameObject _staticCursor;
    public MouseUtility(Camera camera, GameObject staticCursor)
    {
        _camera = camera;
        _staticCursor = staticCursor;
    }

    public void Interact(Vector2 pos) // This position could potentially be a const value, because the mouse is fixed to the middle of the screen. But it could be usefull in the future if the functionality should change.
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.collider != null) 
            {
                hit.collider.GetComponent<IInteractable>()?.Interact();
            }
        }
    }

    public void SetCursorState()
    {
        switch (InputReader.s_State)
        {
            case InputState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case InputState.UI:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
