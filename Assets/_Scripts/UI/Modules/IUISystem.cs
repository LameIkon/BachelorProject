public interface IUISystem // Used to tell systems this is UI element
{
    /// <summary>
    /// An uniq identifier for this specific ui. Must only be one instance of it!
    /// </summary>
    UIType UIType { get; }
    
    /// <summary>
    /// The rules for the ui desides how it should be handled
    /// </summary>
    UIRuleType RuleType { get; } 

    bool IsOpen { get; }

    void Open();
    void Close();
}
