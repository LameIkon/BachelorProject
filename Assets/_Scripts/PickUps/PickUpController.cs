using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class PickUpController : MonoBehaviour, IPickable
{
	private Transform _holdPoint;
	private Rigidbody _rb;

	private Transform _playerTransform;
	private bool _isPickedUp;

	[SerializeField] private PickableType _pickableType;

    public PickableType PickableType => _pickableType;

    public Transform Transform => transform;

    #region Unity Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_rb = GetComponent<Rigidbody>();
        _isPickedUp = false;
    }

  //  // Update is called once per frame
  //  void Update()
  //  {
		//if (_isPickedUp) 
		//{
		//	transform.position = _playerTransform.position;
		//}
  //  }

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
		//_playerTransform = holdPoint;
		//_isPickedUp = !_isPickedUp;
	}

	private void PickUp(Transform holdPoint)
	{
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
	}
	#endregion

}
