using UnityEngine;

public abstract class EventRecordBuilderSO : ScriptableObject
{
    public abstract void Initialize(LevelRecord level);
    public abstract void Apply(LevelRecord level, InteractionEvent eventContext);
}
