using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;
    [SerializeField] private float _beltSpeed = 0;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private List<Rigidbody> _onBelt;


    private void OnEnable()
    {
        _ovenstateChangeEvent.OnRaise += Adjust;
    }

    private void OnDisable()
    {
        _ovenstateChangeEvent.OnRaise -= Adjust;
    }

    private void Adjust(float value)
    {
        _beltSpeed = value;
    }


    #region Unity Methods
    void Update()
    {
        // Optimize later
        Vector3 moveDir = new Vector3(_direction.x, 0, _direction.y).normalized;

        foreach (Rigidbody rb in _onBelt)
        {

            Vector3 targetPos = rb.position + moveDir * _beltSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb);
        _onBelt.Add(rb);
    }

    private void OnTriggerExit(Collider other)
	{
		other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb);
		_onBelt.Remove(rb);
	}
    #endregion
}
