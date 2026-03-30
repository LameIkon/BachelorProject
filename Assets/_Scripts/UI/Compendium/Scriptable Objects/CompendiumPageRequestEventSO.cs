using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Compendium Page Request Event SO", menuName = "ScriptableObject/Compendium/Page Request Event")]
public class CompendiumPageRequestEventSO : ScriptableObject
{
    public event Action<CompendiumID> OnRaise;

    public void Raise(CompendiumID id) => OnRaise?.Invoke(id);
}