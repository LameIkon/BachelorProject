using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Scene Event SO", menuName = "ScriptableObject/Events/Scene")]
public class SceneLoadEventSO : ScriptableObject
{
    public event Action<int> OnRaise;

    public void Raise(int levelId) => OnRaise?.Invoke(levelId);
}
