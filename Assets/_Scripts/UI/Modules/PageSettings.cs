using System;
using UnityEngine;

[Serializable]
public class PageSettings
{
    [Header("Required")]
    [Tooltip("'MultiplePages' mean we will have multiple gameobjects to switch between, while 'Overridepage' means we will change the current gameobject")]
    public PageMode pageMode;
    [Tooltip("Assign a pageContainer if we set a 'Page mode'. Buttons is expected to be part of the page itself but an optional decision below can be used instead for buttons")]
    public GameObject pageContainer;

    [Header("Optional")]
    [Tooltip("An optional container can be set here, if pageContainer is not designed to hold buttons")]
    public GameObject buttonContainer;
}

