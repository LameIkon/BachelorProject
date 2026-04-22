using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// The interface is for interactions
    /// </summary>
    /// <param></param>
    void Interact(Transform transform = null);


    /// <summary>
    /// Data to display ui for helping guiding player for how to interact with objects
    /// </summary>
    /// <returns>HoverData struct containing icon and description</returns>
    //InteractionData GetInteractionData();
}

public struct InteractionData
{
    public string icon;
    public string description;
}
