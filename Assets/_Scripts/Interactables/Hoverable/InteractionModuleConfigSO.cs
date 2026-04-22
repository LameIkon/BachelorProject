using UnityEngine;

public abstract class InteractionModuleConfigSO : ScriptableObject
{
    public abstract InteractionModuleResult Create(GameObject owner);
}