using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Load Data Event SO", menuName = "ScriptableObject/SaveLoad/Load")]
public class LoadDataEventSO : ScriptableObject
{
    public event Func<string, string, Type, ISavableData> OnLoad;

    public T Load<T>(string fileName, string subFolder) where T : class, ISavableData => OnLoad?.Invoke(fileName, subFolder, typeof(T)) as T;
}