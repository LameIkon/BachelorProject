using System.Collections.Generic;

public interface ISavableData
{
    public string SaveFileName { get; }
    public List<string> SaveFolders { get; }
}