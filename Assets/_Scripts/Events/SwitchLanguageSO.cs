using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Switch Language", menuName = "ScriptableObject/Events/Switch Language")]
public class SwitchLanguageSO : ScriptableObject
{
    public event Action<Language> OnRaise;

    public void Raise(Language setLanguage) => OnRaise?.Invoke(setLanguage);
}
