using System;
using UnityEngine;

[Serializable] 
public class LocalizedText
{
    public Language language;
    public string title;
    [TextArea(5,20)] public string description;
}
