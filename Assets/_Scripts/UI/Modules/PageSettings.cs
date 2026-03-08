using System;
using UnityEngine;

[Serializable]
public class PageSettings
{
    [Header("Required")]
    [Tooltip("'MultiplePages' mean we will have multiple gameobjects to switch between while 'Overridepage' means we will change the current gameobject")]
    public PageMode pageMode;
    [Tooltip("Assign a pageContainer if we set a 'Page mode'")]
    public GameObject pageContainer;

    [Header("Optional")]
    [Tooltip("If pageContainer is not designed to hold buttons an optional container can be set here")]
    public GameObject buttonContainer;
}

