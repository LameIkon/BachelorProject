using System;
using UnityEngine;

[Serializable]
public class PageSettings
{
    [Tooltip("'MultiplePages' mean we will have multiple gameobjects to switch between while 'Overridepage' means we will change the current gameobject")]
    public PageMode pageMode;
    [Tooltip("Assign a pageContainer if we set a 'Page mode'")]
    public GameObject pageContainer;
}

