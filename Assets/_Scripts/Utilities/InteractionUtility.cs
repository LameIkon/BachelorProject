using System;
using UnityEngine;

public class InteractionUtility
{
    // Core settings
    private readonly Camera _camera;
    private readonly LayerMask _interactionMask;
    private readonly Transform _pickUpPoint;
    private readonly float _pickUpDistance;

    // Hoverable 
    private IHoverable _currentHovered;
    private IHoverable _newHovered;
    private UIHoverDataEventSO _hoverEvent;


    /// <summary>
    /// How to interact with interactables
    /// </summary>
    /// <param name="camera">Camera reference to do raycast from</param>
    /// <param name="pickUpPoint">Objects that can be picked; Place object in front of player like you are holding it</param>
    /// <param name="interactRange">The range of which you can reach</param>
    public InteractionUtility(Camera camera, Transform pickUpPoint, float interactRange, UIHoverDataEventSO hoverDataEvent)
    {
        // Interaction related
        _camera = camera;
        _pickUpPoint = pickUpPoint;
        _pickUpDistance = interactRange;
        _hoverEvent = hoverDataEvent;
        
        _interactionMask = LayerMask.GetMask("Interactable"); // Specific layer we can interact with

    }

    public void OnUpdate()
    {
        if (InputReader.s_State != InputState.Game) return;
        CrosshairHover(InputReader.MousePos);
    }


    /// <summary>
    /// Inputs for Use, takes the position of the mouse on the screen. 
    /// </summary>
    public void Interact(Vector2 pos)
    {
        if (InputReader.s_State != InputState.Game) return; // Can't interact if we aren't in Game input state

        if (RaycastInteractable(pos, out RaycastResult result))
        {
            result.Interactable?.Interact(_pickUpPoint);
        }
    }


    private void CrosshairHover(Vector2 pos)
    {
        _newHovered = null;

        if (RaycastInteractable(pos, out RaycastResult result))
        {
            //Debug.Log(result.Hoverable);
            _newHovered = result.Hoverable;

        }


        if (_currentHovered != null && _currentHovered != _newHovered) // Exit old
        {
            _currentHovered.OnHoverExit();
            _currentHovered = null;
        }

        if (_newHovered != null && _newHovered != _currentHovered) // Enter new
        {
            _newHovered.OnHoverEnter();

            if (_newHovered is IInteractable interactable)
            {
                _hoverEvent?.Raise(interactable.GetInteractionData()); // Update ActionGuide ui to display the required options for interaction
            }
            _currentHovered = _newHovered;
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
    private bool RaycastInteractable(Vector2 screenPos, out RaycastResult result)
    {
        Ray ray = _camera.ScreenPointToRay(screenPos);

        result = new RaycastResult();

        if (Physics.Raycast(ray, out RaycastHit hit, _pickUpDistance, _interactionMask))
        {
            
            // Optional Ihoverable detection found
            if (hit.collider.TryGetComponent(out IHoverable foundHoverable))
            {
                result.Hoverable = foundHoverable;
            }
            
            // Try get interactable interface
            if (hit.collider.TryGetComponent(out IPickable pickable)) // Try get IPickable first
            {
                result.Interactable = pickable;
            }
            else if (hit.collider.TryGetComponent(out IInteractable normalInteractable)) // Otherwise get IInteractable
            {
                result.Interactable = normalInteractable;
            }
        }

        return result.Interactable != null || result.Hoverable != null;
    }

    /// <summary>
    /// Used for Raycast Detection to store Ihoverable and IInterable interfaces
    /// </summary>
    private struct RaycastResult
    {
        public IInteractable Interactable;
        public IHoverable Hoverable;
    }
}
