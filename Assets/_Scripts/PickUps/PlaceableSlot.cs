using UnityEngine;

public class PlaceableSlot : MonoBehaviour
{
    [SerializeField] private PickableType _pickableTypeHolder; 
    [SerializeField] private Transform _visualModel;
    [SerializeField] private Transform _snapPoint;
    
    private bool _hasAssignedSlot = false;
    private IPickable _pickupInTrigger; // The pickup currently inside this slot

    [SerializeField] private Material _visualMaterial;

    private void Awake()
    {
        if (_visualModel.TryGetComponent(out Renderer renderer))
        {
            _visualMaterial = renderer.material;
        }
    }


    # region Placement
    public void TryPlace(IPickable pickup)
    {
        if (_hasAssignedSlot) return;

        if (pickup == _pickupInTrigger)
        {
            AssignToSlot(pickup);
        }
    }
    private void AssignToSlot(IPickable pickup)
    {
        if (!_hasAssignedSlot)
        {
            _hasAssignedSlot = true;

            Transform pickupTransform = pickup.Transform;

            // Parent it to this slot
            pickupTransform.SetParent(_snapPoint);

            // Snap into position
            pickupTransform.localPosition = Vector3.zero;
            pickupTransform.localRotation = Quaternion.identity;


            //// Stop physics
            //if (pickupTransform.TryGetComponent(out Rigidbody rb))
            //{
            //    rb.isKinematic = true;
            //}

            // Disable visual indication
            //_visualModel.gameObject.SetActive(false);
            _visualMaterial.SetFloat("_Alpha", 0f);

            Debug.Log("Assigned");
        }
    }

    #endregion
    #region Trigger methods
    private void OnTriggerEnter(Collider other)
    {
        if (_hasAssignedSlot) return; // slot already occupied

        if (other.TryGetComponent(out IPickable pickup))
        {
            if (pickup.PickableType == _pickableTypeHolder)
            {
                Debug.Log("this can be placed here");
                _pickupInTrigger = pickup; // Placable pickup detected
                _visualMaterial.SetFloat("_Alpha", 0.4f);
            }
            else
            {
                Debug.Log("This can not be placed here");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IPickable pickup))
        {
            if (_pickupInTrigger == pickup) // Compare if the same that entered
            {
                _hasAssignedSlot = false;
                _pickupInTrigger = null; // clear when leaving

                // Disable visual indication
                //_visualModel.gameObject.SetActive(true);
                _visualMaterial.SetFloat("_Alpha", 0.25f);

                Debug.Log("Pickup unassigned and can now be used again"); 
            }
        }        
    }
    #endregion
}
