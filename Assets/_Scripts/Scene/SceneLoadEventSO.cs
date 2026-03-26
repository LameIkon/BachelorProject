using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Scene Event SO", menuName = "ScriptableObject/Events/Scene")]
public class SceneLoadEventSO : ScriptableObject
{
    public event Action<LevelName> OnRaise;

    public void Raise(LevelName name) => OnRaise?.Invoke(name);
}
