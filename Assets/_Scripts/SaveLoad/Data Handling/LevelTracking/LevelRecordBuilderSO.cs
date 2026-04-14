using UnityEngine;

public abstract class LevelRecordBuilderSO : ScriptableObject
{
    public abstract void Initialize(LevelRecord level);
    public abstract void Apply(LevelRecord level, InteractionEvent eventContext);
}
