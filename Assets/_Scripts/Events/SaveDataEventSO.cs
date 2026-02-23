using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Save Data Event SO", menuName = "ScriptableObject/Events/SaveLoad/Save")]
public class SaveDataEventSO : ScriptableObject
{
    public event Action<ISavableData> OnSave;

    public void Save(ISavableData data) => OnSave?.Invoke(data);
}
