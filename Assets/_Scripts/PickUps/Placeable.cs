using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private PickableType _pickableTypeHolder;
    [SerializeField] private GameObject _highlightHolder;
    [SerializeField] private GameObject _placed;



    private void Check(PickableType? type)
    {
        if (type == null) return;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.TryGetComponent(out IPickable pickup))
        {
            if (pickup.PickableType == _pickableTypeHolder)
            {
                Debug.Log("this can be placed here");
            }
            else
            {
                Debug.Log("This can not be placed here");
            }
        }
    }

}
