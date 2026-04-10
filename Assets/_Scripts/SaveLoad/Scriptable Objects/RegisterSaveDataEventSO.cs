using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Register Save Data Event SO", menuName = "ScriptableObject/SaveLoad/Register")]
public class RegisterSaveDataEventSO : ScriptableObject
{
    public event Action<ISavableData> OnSave;

    public void Save(ISavableData data) => OnSave?.Invoke(data);
}
