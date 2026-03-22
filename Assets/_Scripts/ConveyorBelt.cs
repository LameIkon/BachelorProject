using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;
    [SerializeField] private float _beltSpeed = 0;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private List<GameObject> _onBelt;

    private float _maxSpeed = 0.25f;

    private void OnEnable()
    {
        _ovenstateChangeEvent.OnRaise += Adjust;
    }

    private void OnDisable()
    {
        _ovenstateChangeEvent.OnRaise -= Adjust;
    }

    private void Adjust(OvenStatus status)
    {
        switch (status)
        {
            case OvenStatus.Stop:
                _beltSpeed = 0;
                break;
            case OvenStatus.Increase:
                if (_beltSpeed < _maxSpeed) _beltSpeed += 0.01f;
                break;
            case OvenStatus.Decrease:
                if (_beltSpeed > 0) _beltSpeed -= 0.01f;
                break;
        }
    }


    #region Unity Methods
    void Update()
    {
        // Optimize later
        Vector3 moveDir = new Vector3(_direction.x, 0, _direction.y).normalized;

        foreach (GameObject obj in _onBelt)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                Vector3 targetPos = rb.position + moveDir * _beltSpeed * Time.fixedDeltaTime;
                rb.MovePosition(targetPos);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        _onBelt.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _onBelt.Remove(other.gameObject);
    }
    #endregion
}
