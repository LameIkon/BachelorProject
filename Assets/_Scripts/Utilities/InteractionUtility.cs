using Unity.VisualScripting;
using UnityEngine;

public class InteractionUtility
{
    private readonly Camera _camera;
    private readonly LayerMask _interactionMask;
    private readonly Transform _pickUpPoint;
    private readonly float _pickUpDistance;

    private readonly GameObject _crosshairUI;

    /// <summary>
    /// How to interact with interactables
    /// </summary>
    /// <param name="camera">Camera reference to do raycast from</param>
    /// <param name="pickUpPoint">Objects that can be picked; Place object in front of player like you are holding it</param>
    /// <param name="interactRange">The range of which you can reach</param>
    public InteractionUtility(Camera camera, Transform pickUpPoint, float interactRange, GameObject crosshair)
    {
        // Interaction related
        _camera = camera;
        _pickUpPoint = pickUpPoint;
        _pickUpDistance = interactRange;
        
        _interactionMask = LayerMask.GetMask("Interactable"); // Specific layer we can interact with

        // Crosshair related
        _crosshairUI = crosshair;

        InputReader.s_OnInputStateChangedEvent += SetCursorState;
        SetCursorState(InputReader.s_State);
    }

    /// <summary>
    /// Inputs for Use, takes the position of the mouse on the screen. 
    /// </summary>
    public void Interact(Vector2 pos)
    {
        if (InputReader.s_State != InputState.Game) return; // Can't interact if we aren't in Game input state

        Ray ray = _camera.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit, _pickUpDistance, _interactionMask))
        {
            if (hit.collider.TryGetComponent(out IPickable pickable))
            {
                pickable.Interact(_pickUpPoint);
                return;
            }

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }
    }

    /// <summary>
    /// Need to be in an update method to detect whenever an object is being hovered over
    /// </summary>
    public void OnUpdate()
    {
        if(InputReader.s_State != InputState.Game) return;
    }

    #region Mouse Methods

    
    /// <summary>
    /// Set input reading state. Can change between Game and UI mode
    /// </summary>
    private void SetCursorState(InputState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case InputState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                _crosshairUI.SetActive(true);
                break;
            case InputState.UI:
                Cursor.lockState = CursorLockMode.None;
                _crosshairUI.SetActive(false);
                break;
        }
    }
    #endregion

    #region Disable
    /// <summary>
    /// Call this method from the coupled script to disable events, when we don't need to listen anymore
    /// </summary>
    public void Dispose()
    {
        InputReader.s_OnInputStateChangedEvent -= SetCursorState;
    }
    #endregion
}
