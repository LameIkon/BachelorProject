
/// <summary>
/// Works together with Ipickable. An enum PickableType is used to address specific locations to drop pickables if any assigned
/// </summary>
public interface IPlaceable
{
    PickableType PickableType { get; }
}
