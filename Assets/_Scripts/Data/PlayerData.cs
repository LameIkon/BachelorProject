using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Player")]
public class PlayerData : ScriptableObject 
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxMovementSpeed;

    [SerializeField, Range(1, 1000)] private float _mouseSpeed;



    public float MovementSpeed => _movementSpeed;
    public float MaxMovementSpeed => _maxMovementSpeed;
    public float MouseSpeed => _mouseSpeed/5000f; // This is done becase the sensitivity, quickly changes.


}
