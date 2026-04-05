using UnityEngine;
using Random = UnityEngine.Random;

public class PlaceableSlot : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private PickableType _pickableTypeHolder; 
    [SerializeField] private Transform _visualModel;
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private float _wiggleStrenght;
    
    private IPickable _pickupInTrigger; // The pickup currently inside this slot
    private IPickable _placedPickup; // The pickup placed

    private Material _visualMaterial;

    [Header("Options")]
    [SerializeField] private bool _canPlaceOnce;
    [SerializeField] private bool _setToKinematic;

    private bool _canPlace;
    //private bool _isOccupied;

    private bool IsOccupied => _placedPickup != null;

    private void Awake()
    {
        if (_visualModel.TryGetComponent(out Renderer renderer))
        {
            _visualMaterial = renderer.material;
        }
        _wiggleStrenght = 1f;
        _canPlace = true;
    }


    # region Placement
    public void TryPlace(IPickable pickup)
    {
        if (!_canPlace || IsOccupied) return;

        if (pickup == _pickupInTrigger)
        {
            AssignToSlot(pickup);
        }
    }
    private void AssignToSlot(IPickable pickup)
    {
        Transform pickupTransform = pickup.Transform;

        // Parent it to this slot
        pickupTransform.SetParent(_snapPoint);

        // Snap into position
        pickupTransform.localPosition = Vector3.zero;
        pickupTransform.localRotation = Quaternion.identity;


        // Adjust physics
        if (pickupTransform.TryGetComponent(out Rigidbody rb))
        {
            rb.MovePosition(transform.position);
            rb.MoveRotation(transform.rotation);
            rb.angularVelocity = new Vector3(RandomNumber(), 0, RandomNumber()); // Polish makes cube jitter when placed
            rb.linearVelocity = Vector3.zero;

            if (_setToKinematic) rb.isKinematic = true;
        }

        // Disable visual indication
        SetVisualAlpha(0f);

        // Set states
        _placedPickup = pickup;

        if (_canPlaceOnce)
        {
            _canPlace = false;
        }

        Debug.Log("Assigned");
        
    }

    private void RemoveFromSlot()
    {
        if (!IsOccupied) return;

        // Enable physics
        if (_setToKinematic && _placedPickup.Transform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        
        _placedPickup = null;

        if (_canPlaceOnce)
        {
            _canPlace = false;
            SetVisualAlpha(0f);
        }
        else
        {
            _canPlace = true;
            SetVisualAlpha(0.25f);

        }
    }

    #endregion

    #region Helpers
    private float RandomNumber() 
    {
        return Random.Range(-_wiggleStrenght, _wiggleStrenght);
    }

    public void EnableCanPlace(bool canPlace, bool canPlaceOne = true) // Called from outside if we want to adjust the settings
    {
        _canPlace = canPlace;
        _canPlaceOnce = canPlaceOne;

        if (!IsOccupied && _pickupInTrigger == null)
        {
            // Dim visual indication
            SetVisualAlpha(0.25f);
        }

    }

    private void SetVisualAlpha(float value)
    {
        if (_visualMaterial == null) return;

        _visualMaterial.SetFloat("_Alpha", value);
    }

    #endregion

    #region Trigger methods

    private void OnTriggerEnter(Collider other)
    {
        if (IsOccupied || !_canPlace) return;

        if (other.TryGetComponent(out IPickable pickup))
        {
            if (pickup.PickableType == _pickableTypeHolder)
            {
                _pickupInTrigger = pickup; // Placable pickup detected
                SetVisualAlpha(0.4f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IPickable pickup)) return;

        if (_placedPickup == pickup) // Compare if the placed pickup that exited
        {
            RemoveFromSlot();
            return;
        }

        if (_pickupInTrigger == pickup) // If it is an canditate that never got placed but entered
        {
            _pickupInTrigger = null;
            if (!IsOccupied && _canPlace) SetVisualAlpha(0.25f);
        }       
    }
    #endregion
}
