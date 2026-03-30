using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class InteractablePickup : MonoBehaviour, IPickable, IHoverable
{
	[Header("Events")]
	[SerializeField] private UIToggleEventSO _uiToggleEvent;
	[SerializeField] private CompendiumPageRequestEventSO _pageRequestEvent;

	[Header("Data type")]
	[SerializeField] private PickableType _pickableType;
	[SerializeField] private CompendiumID _compendiumID;
	
	// Handlers
	private HighlightHandler _highlightHandler;
	private InteractionMenuHandler _interactionMenuHandler;
	
	private Transform _holdPoint;
	private Rigidbody _rb;

	private bool _isPickedUp;
	private bool _canBePickedUp = true;


    public PickableType PickableType => _pickableType;

	private PlaceableSlot _currentSlot;

    public Transform Transform => transform;


    #region Unity Methods

    private void Awake()
    {
		_rb = GetComponent<Rigidbody>();
		_highlightHandler = new HighlightHandler(this.gameObject);
		_interactionMenuHandler = new InteractionMenuHandler(_uiToggleEvent, _pageRequestEvent, _compendiumID);

        _isPickedUp = false;      
    }

    private void FixedUpdate()
    {
        if (_isPickedUp)
		{
			Vector3 direction = _holdPoint.position - _rb.position;

			float followSpeed = 20f;
			_rb.linearVelocity = direction * followSpeed;
		}
    }
    #endregion


    #region Interactable interface
    public void Interact(Transform holdPoint)
	{
		if (!_isPickedUp)
		{
			PickUp(holdPoint);
		}
		else
		{
			Drop();
		}
	}

	private void PickUp(Transform holdPoint)
	{
		if (_highlightHandler != null)
		{
			_highlightHandler.SetHighlight(false);
		}

		transform.SetParent(null);
		_holdPoint = holdPoint;
        _isPickedUp = true;
		
        _rb.useGravity = false;
        _rb.linearDamping = 10f;
	}

	public void Drop()
	{

        _isPickedUp = false;
        _holdPoint = null;

        _rb.useGravity = true;
		_rb.linearDamping = 0f;
		Debug.Log("Drop");


		if (_highlightHandler != null && _canBePickedUp)
		{
			_highlightHandler.SetHighlight(true);
		}

		_currentSlot?.TryPlace(this);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlaceableSlot slot))
		{
			_currentSlot = slot; // store the slot in the pickup
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out PlaceableSlot slot))
		{
			if (_currentSlot == slot) _currentSlot = null;
		}
	}
	#endregion

    #region Hovering logic
    public void OnHoverEnter()
	{
		if (_isPickedUp) return;
		_interactionMenuHandler?.OnHoverState(true);
		_highlightHandler?.SetHighlight(true);
	}


    public void OnHoverExit()
	{
		if (_isPickedUp) return;
		Debug.Log("exit hover");
		_interactionMenuHandler?.OnHoverState(false);
		_highlightHandler?.SetHighlight(false);
	}
    #endregion


    #region Cleanup
	private void OnDestroy()
	{
		_interactionMenuHandler?.Dispose();
	}
    #endregion

}
