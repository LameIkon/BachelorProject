using UnityEngine;
using Random = UnityEngine.Random;

public class PlaceableSlot : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private PickableType _allowedType; 
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private Transform _visualModel;
    [SerializeField] private float _wiggleStrenght;


    [Header("Options")]
    [SerializeField] private bool _canPlaceOnce;
    [SerializeField] private bool _setToKinematic;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;

    private Transform _placed;
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
    public bool TryPlace(InteractionIdentitySO identity, Transform target)
    {
        if (!_canPlace) return false;
        if (_placed != null) return false;
        if (identity.type != _allowedType) return false;
        
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

        if (_canPlaceOnce)
        {
            _canPlace = false;
        }

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
        _canPlaceOnce = canPlaceOne;

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

    #endregion

    #region Trigger methods

    public void OnCandidateEnter(InteractionIdentitySO identity)
    {
        if (_placed != null || !_canPlace) return;

        if (identity.type == _allowedType)
        {
            SetVisualAlpha(0.4f);
        }
    }

    public void OnCandidateExit(InteractionIdentitySO identity, Transform target)
    {
        if (_placed == null) return;
        if (target != _placed) return;

        if (_setToKinematic && _placed.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        
        _placed = null;

        if (_canPlaceOnce) return;

        else
        {
            _canPlace = true;
            SetVisualAlpha(0.25f);

        }
    }
    #endregion
}
