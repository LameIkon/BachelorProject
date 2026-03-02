using System;

public interface IUISystem // Used to tell systems this is UI element
{
    UIType UIType { get; }
    bool IsOpen { get; }

    void Close();
}
