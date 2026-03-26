using UnityEngine;

public class InteractionUtility
{
    private readonly Camera _camera;
    private readonly LayerMask _interactionMask;
    private readonly Transform _pickUpPoint;
    private readonly float _pickUpDistance;

    /// <summary>
    /// How to interact with interactables
    /// </summary>
    /// <param name="camera">Camera reference to do raycast from</param>
    /// <param name="pickUpPoint">Objects that can be picked; Place object in front of player like you are holding it</param>
    /// <param name="interactRange">The range of which you can reach</param>
    public InteractionUtility(Camera camera, Transform pickUpPoint, float interactRange)
    {
        // Interaction related
        _camera = camera;
        _pickUpPoint = pickUpPoint;
        _pickUpDistance = interactRange;
        
        _interactionMask = LayerMask.GetMask("Interactable"); // Specific layer we can interact with

        // Mouse position
        InputReader.s_OnMouseMoveEvent += Hover;
    }

    /// <summary>
    /// Inputs for Use, takes the position of the mouse on the screen. 
    /// </summary>
    public void Interact(Vector2 pos)
    {
        if (InputReader.s_State != InputState.Game) return; // Can't interact if we aren't in Game input state

        if (RaycastInteractable(pos, out IPickable pickable, out IInteractable interactable))
        {
            pickable?.Interact(_pickUpPoint);
            interactable?.Interact();
        }
    }

    private void Hover(Vector2 pos)
    {
        if (InputReader.s_State != InputState.Game) return; // Can't interact if we aren't in Game input state

        if (RaycastInteractable(pos, out IPickable pickable, out IInteractable interactable))
        {
            if (pickable != null)
            {
                Debug.Log(pickable);
            }
            if (interactable != null)
            {
                Debug.Log(interactable);
            }
        }
    }

    /// <summary>
    /// A boolean detection for the interactable objects in the game the player can interact with. If any interactables found return true
    /// And give out the interactable. Will only return one to work with. IPickable has priority
    /// </summary>
    /// <param name="ray">Raycast from center of camera</param>
    /// <param name="pickable">Types that the player can pick up</param>
    /// <param name="interactable">Types the player can interact with, eg. press a button</param>
    /// <returns></returns>
    private bool RaycastInteractable(Vector2 screenPos, out IPickable pickable, out IInteractable interactable)
    {
        Ray ray = _camera.ScreenPointToRay(screenPos);

        pickable = null;
        interactable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, _pickUpDistance, _interactionMask))
        {
            pickable = hit.collider.GetComponent<IPickable>();
            interactable = hit.collider.GetComponent<IInteractable>();

            return pickable != null || interactable != null;
        }

        return false;
    }
}
