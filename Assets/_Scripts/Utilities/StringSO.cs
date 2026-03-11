using UnityEngine;
/// <summary>
/// A simple Scriptable Object to hold a string
/// </summary>

[CreateAssetMenu(fileName ="String SO", menuName = "ScriptableObject/Variable/String Data")]
public class StringSO : ScriptableObject
{
    public string stringData;
}
