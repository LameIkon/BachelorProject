using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Scene Event SO", menuName = "ScriptableObject/Events/Scene")]
public class SceneLoadEventSO : ScriptableObject
{
    public event Action<LevelData> OnRaise;

    public void Raise(LevelData levelData) => OnRaise?.Invoke(levelData);
}
