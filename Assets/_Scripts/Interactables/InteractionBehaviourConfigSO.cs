using System;
using UnityEngine;

public abstract class InteractionBehaviourConfigSO : ScriptableObject
{
    public abstract InteractionModuleResult Create(GameObject owner, InteractionIdentitySO identity, IInteractionEvent interactEvent);
}