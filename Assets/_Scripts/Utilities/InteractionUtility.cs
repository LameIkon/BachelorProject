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
        _camera = camera;
        _pickUpPoint = pickUpPoint;
        _pickUpDistance = interactRange;
        
        _interactionMask = LayerMask.GetMask("Interactable"); // Specific layer we can interact with
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
            hit.collider.GetComponent<IInteractable>()?.Interact(_pickUpPoint);
        }
    }
}
