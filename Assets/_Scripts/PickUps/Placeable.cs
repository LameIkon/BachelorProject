using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private PickableType _pickableTypeHolder; 
    [SerializeField] private Transform _visualModel;
    [SerializeField] private Transform _snapPoint;
    
    private bool _hasAssignedSlot = false;
    private IPickable _pickupInTrigger; // The pickup currently inside this slot

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


            // Stop physics
            if (pickupTransform.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
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
                //AssignToSlot(pickup);
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
                _pickupInTrigger = null; // clear when leaving
            }
        }        
    }
    #endregion
}
