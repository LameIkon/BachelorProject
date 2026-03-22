using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private PickableType _pickableTypeHolder; 
    [SerializeField] private Transform _visualModel;
    [SerializeField] private Transform _snapPoint;
    private bool _hasAssignedSlot;

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

            Debug.Log("Assigned");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPickable pickup))
        {
            if (pickup.PickableType == _pickableTypeHolder)
            {
                Debug.Log("this can be placed here");
                AssignToSlot(pickup);
            }
            else
            {
                Debug.Log("This can not be placed here");
            }
        }
    }

}
