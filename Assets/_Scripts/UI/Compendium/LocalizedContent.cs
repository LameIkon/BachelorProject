using System;
using UnityEngine;

[Serializable] 
public class LocalizedContent
{
    public string title;
    [TextArea(5,20)]public string description;
}
