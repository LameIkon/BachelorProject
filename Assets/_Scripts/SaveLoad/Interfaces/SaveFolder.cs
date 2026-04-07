using System;
using System.Collections.Generic;

[Serializable]
public class SaveFolder
{
    public string Name;
    public List<SaveFolder> Subfolders = new();

    public SaveFolder(string name)
    {
        Name = name;
    }
}