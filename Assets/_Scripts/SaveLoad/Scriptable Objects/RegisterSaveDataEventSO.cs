using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Save Data Event SO", menuName = "ScriptableObject/SaveLoad/Save")]
public class RegisterSaveDataEventSO : ScriptableObject
{
    public event Action<ISavableData> OnSave;

    public void Save(ISavableData data) => OnSave?.Invoke(data);
}


[CreateAssetMenu(fileName ="Get Data Event SO", menuName = "ScriptableObject/SaveLoad/Get Data")]
public class GetDataEventSO : ScriptableObject
{
    public event Action<InteractionEvent> OnRaise;

    public void Save(InteractionEvent context) => OnRaise?.Invoke(context);
}
