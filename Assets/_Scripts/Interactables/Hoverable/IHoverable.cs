/// <summary>
/// Assigned to objects that needs hover detection from raycast
/// </summary>
public interface IHoverable
{
    /// <summary>
	/// InteractionUtility will handle the detection
	/// </summary>
    void OnHoverEnter();

    /// <summary>
	/// InteractionUtility will handle the detection
	/// </summary>
    void OnHoverExit();
}

