using UnityEngine;
/// <summary>
/// The interface is for interactions which can be picked up by the player. An enum PickableType is used to address specific locations to drop pickables if any assigned
/// </summary>
public interface IPickable
{
    void Interact(Transform transform);
    void Drop();

    PickableType PickableType { get; }
    Transform Transform { get; }
}
