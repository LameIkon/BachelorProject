using System;
using System.Collections.Generic;

[Serializable]
public class SaveFolder
{
    public string Name;
    public List<SaveFolder> Subfolders = new();
    public List<ISavableData> Files = new();

    public SaveFolder(string name)
    {
        Name = name;
    }
}