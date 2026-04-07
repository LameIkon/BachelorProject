using System.Collections.Generic;

public interface ISavableData
{
    public string SaveFileName { get; }
    List<string> SavePath { get; }
}
