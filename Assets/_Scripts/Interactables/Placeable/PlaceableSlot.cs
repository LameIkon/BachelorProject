using UnityEngine;
using Random = UnityEngine.Random;

public class PlaceableSlot : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private PickableType _allowedType; 
    [SerializeField] private Transform _snapPoint;
    
    [SerializeField] private Transform _visualModel;
    [SerializeField] private float _wiggleStrenght;
    
    private IPickableIdentity _candidate;
    private IPickableIdentity _placed;

    private Material _visualMaterial;

    [Header("Options")]
    [SerializeField] private bool _canPlaceOnce;
    [SerializeField] private bool _setToKinematic;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;

    private bool _canPlace;

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
    public bool TryPlace(IPickableIdentity pickup)
    {
        if (!_canPlace) return false;
        if (pickup == null) return false;

        if (pickup.type != _allowedType) return false;
        {
            AssignToSlot(pickup);
            return true;
        }
    }
    private void AssignToSlot(IPickableIdentity pickup)
    {
        _placed = pickup;

        Transform pickupTransform = pickup.transform;

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

        if (_canPlaceOnce)
        {
            _canPlace = false;
        }

        Debug.Log("Assigned");

        if (_questCompleteEvent != null) _questCompleteEvent.Raise(QuestID.PlaceItem);
    }

    private void RemoveFromSlot()
    {
        if (_placed == null) return;

        // Enable physics
        if (_setToKinematic && _placed.transform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        
        _placed = null;

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

        if (_placed == null && _candidate == null)
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
        if (other.TryGetComponent(out IPickableIdentity pickup))
        {
            if (pickup.type == _allowedType) _candidate = pickup;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IPickableIdentity pickup))
        {
            if (_candidate == pickup) _candidate = null;
        }
    }
    #endregion
}
