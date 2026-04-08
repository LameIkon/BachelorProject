using System.Collections.Generic;

public interface ISavableData
{
    public string SaveFileName { get; }

    /// <summary>
    /// Path of folders. Each folder added to the list will become a subfolder of the previous
    /// </summary>
    List<string> SavePath { get; }
}
