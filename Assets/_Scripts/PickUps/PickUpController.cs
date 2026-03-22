using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class PickUpController : MonoBehaviour, IPickable
{

	private Transform _playerTransform;
	private bool _isPickedUp;

	[SerializeField] private PickableType _pickableType;

    public PickableType PickableType => _pickableType;

    #region Unity Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (_isPickedUp) 
		{
			transform.position = _playerTransform.position;
		}
    }
	#endregion


	#region Interactable interface
	public void Interact(Transform transform)
	{
		_playerTransform = transform;
		_isPickedUp = !_isPickedUp;
	}
	#endregion

}
