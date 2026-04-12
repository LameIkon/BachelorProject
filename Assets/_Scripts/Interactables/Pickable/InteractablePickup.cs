using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class InteractablePickup : HoverableInteractable, IPickable
{
	[Header("Events")]
	[SerializeField] private StoreDataEventSO _storeDataEvent;

	[Header("Pickup Settings")]
	[SerializeField] private PickableType _pickableType;
	[SerializeField] private bool _disablePickupOnPlacement;
	
	private Transform _holdPoint;
	private Rigidbody _rb;

	private bool _isPickedUp;
	private bool _canBePickedUp = true;


    public PickableType PickableType => _pickableType;

	private PlaceableSlot _currentSlot;

    public Transform Transform => transform;


    #region Unity Methods

    protected override void Awake()
    {
		base.Awake();

		_rb = GetComponent<Rigidbody>();
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
		if (!_canBePickedUp) return;

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
		_highlightHandler?.SetHighlight(false);
		
		transform.SetParent(null);
		_holdPoint = holdPoint;
        _isPickedUp = true;
		
        _rb.useGravity = false;
        _rb.linearDamping = 10f;

		RaiseInteractionEvent(PickableAction.Collected);
	}

	public void Drop()
	{
        _isPickedUp = false;
        _holdPoint = null;

        _rb.useGravity = true;
		_rb.linearDamping = 0f;
		Debug.Log("Drop");


		if (_canBePickedUp) _highlightHandler?.SetHighlight(true);

		bool? state = _currentSlot?.TryPlace(this);

		if (state == true)
		{
			if (_disablePickupOnPlacement)
			{
				base.OnHoverExit();
				_canBePickedUp = false;
			}

			RaiseInteractionEvent(PickableAction.PlacedInSlot);
		}
		else
		{
			RaiseInteractionEvent(PickableAction.Dropped);
		}
	}

    #region Raise Event
	private void RaiseInteractionEvent(PickableAction action)
	{
		InteractionEvent interaction = new InteractionEvent
		{
			eventType = EventType.Pickable,
			pickableType = _pickableType,
			pickableAction = action
		};

		_storeDataEvent.Raise(interaction);
	}

    #endregion


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
    public override void OnHoverEnter()
	{
		if (_isPickedUp || !_canBePickedUp) return;
		base.OnHoverEnter();
	}


    public override void OnHoverExit()
	{
		if (_isPickedUp || !_canBePickedUp) return;
		base.OnHoverExit();
	}
    #endregion
}
