using System.Collections.Generic;

/// <summary>
/// For data storage
/// </summary>
public class DataRecordTracker
{
    private readonly List<InteractionEvent> _eventContext = new();

    public void Add(InteractionEvent context)
    {
        _eventContext.Add(context);
    }

    public List<InteractionEvent> GetData()
    {
        return _eventContext;
    }

    public void Clear()
    {
        _eventContext.Clear();
    }
}
