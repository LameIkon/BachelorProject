using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float _beltSpeed;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private List<GameObject> _onBelt;

    // Update is called once per frame
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
}
