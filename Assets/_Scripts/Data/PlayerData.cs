using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerData : ScriptableObject 
{
    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _mouseSpeed;


    public float MovementSpeed => _movementSpeed;
    public float MouseSpeed => _mouseSpeed;


}
