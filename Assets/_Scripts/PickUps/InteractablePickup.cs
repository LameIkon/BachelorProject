using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class InteractablePickup : MonoBehaviour, IPickable, IHoverable
{
	[SerializeField] private PickableType _pickableType;
	
	// materials
	private Material[] _materials;
	private float _highlightScale;
	
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
		_materials = GetComponent<MeshRenderer>().materials;

		_highlightScale = _materials[1].GetFloat("_OutlineScale");
		_materials[1].SetFloat("", 0f);

        _isPickedUp = false;      
		SetHighlight(_isPickedUp);
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
		SetHighlight(false);
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

		if (_canBePickedUp) SetHighlight(true);; // Temporary
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

	/// <summary>
	/// Indicate if you are hovering over an interactable 
	/// </summary>
	/// <param name="state">boolean to check state. True will activate and false to deactivate highlight</param>
	public void SetHighlight(bool state)
	{
		_materials[1].SetFloat("_OutlineScale", state ? _highlightScale : 0f);
	}


    public void OnHoverEnter()
	{
		if (_isPickedUp) return;
		SetHighlight(true);
	}


    public void OnHoverExit()
	{
		SetHighlight(false);
	}
    #endregion

}
