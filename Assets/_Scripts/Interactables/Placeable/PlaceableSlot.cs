using UnityEngine;

public class PlaceableSlot : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private PickableType _allowedType; 
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private Transform _visualModel;
    [SerializeField] private float _wiggleStrenght;


    [Header("Options")]
    [SerializeField] private bool _placeOnlyOnce;
    [SerializeField] private bool _setToKinematic;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;

    private PickupInteractionDefinitionSO _currentCandidate; // An possible candidate to be placed in slot
    private Transform _placed; // Object placed in slot

    private bool _canPlace;
    private Material _visualMaterial;

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
    public bool TryPlace(PickupInteractionDefinitionSO definition, Transform target)
    {
        if (!_canPlace) return false; // if we are even allowed to place
        if (_placed != null) return false; // if slot occupied
        if (definition.type != _allowedType) return false; // if it's of the type to allow placement
        
        AssignToSlot(target);
        return true;
        
    }
    private void AssignToSlot(Transform pickup)
    {
        _placed = pickup;

        Transform pickupTransform = pickup;

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

        if (_placeOnlyOnce)
        {
            _canPlace = false;
        }

        _currentCandidate = null;
        Debug.Log("Assigned");

        if (_questCompleteEvent != null) _questCompleteEvent.Raise(QuestID.PlaceItem);
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
        _placeOnlyOnce = canPlaceOne;

        if (_placed == null)
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

   private void RemovePlaced()
    {
        if (_placed == null) return;

        if (_setToKinematic && _placed.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        _placed = null;

        if (_canPlace)
        {
            SetVisualAlpha(0.25f);
        }
    }

    #endregion

    #region Trigger methods

    public void OnSlotEnter(PickupInteractionDefinitionSO definition)
    {
        if (_placed != null || !_canPlace) return;
        if (definition.type != _allowedType) return;
        
        _currentCandidate = definition;
        SetVisualAlpha(0.4f);    
    }

    public void OnSlotExit(PickupInteractionDefinitionSO definition, Transform target)
    {
        if (_currentCandidate != null && _currentCandidate.type == definition.type)
        {
            _currentCandidate = null;

            if (_placed == null && _canPlace)
            {
                SetVisualAlpha(0.25f);
            }
        }

        if (_placed != null && target == _placed)
        {
            RemovePlaced();
        }
    }
    #endregion
}
