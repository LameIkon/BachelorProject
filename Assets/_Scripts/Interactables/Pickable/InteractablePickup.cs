using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class InteractablePickup : HoverableInteractable, IPickable
{
	[Header("Pickup Settings")]
	[SerializeField] private PickableType _pickableType;
	
	
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
	}

	public void Drop()
	{
        _isPickedUp = false;
        _holdPoint = null;

        _rb.useGravity = true;
		_rb.linearDamping = 0f;
		Debug.Log("Drop");


		if (_canBePickedUp) _highlightHandler?.SetHighlight(true);

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
    public override void OnHoverEnter()
	{
		if (_isPickedUp) return;
		base.OnHoverEnter();
	}


    public override void OnHoverExit()
	{
		if (_isPickedUp) return;
		base.OnHoverExit();
	}
    #endregion
}
