using System;
using UnityEngine;

[Serializable]
public class CompendiumContent
{
    public DanishSerialization _danishSerialization;
    public EnglishSerialization _englishSerialization;
}

[Serializable] 
public class DanishSerialization
{
    public string title;
    [TextArea(5,20)]public string description;
}

[Serializable] 
public class EnglishSerialization
{
    public string title;
    [TextArea(5,20)]public string description;
}
