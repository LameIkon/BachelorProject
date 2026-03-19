using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// The interface is for interactions, if call with no parameter then it defaults to null.
    /// </summary>
    /// <param name="transform">Optional: The trasform of where objects should go if pick up.</param>
    void Interact(Transform transform = null);
}
