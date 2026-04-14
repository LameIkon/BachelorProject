using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pickable Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Pickable Record")]
public class PickableRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.pickableTypeRecords = new List<PickableTypeRecord>();

        // For more efficient lookup
        level.pickableTypeLookup = new Dictionary<PickableType, PickableTypeRecord>();

        foreach (PickableType type in Enum.GetValues(typeof(PickableType)))
        {
            PickableTypeRecord record = new PickableTypeRecord()
            {
                type = type.ToString(),
                collected = 0,
                dropped = 0,
                placedInSlot = 0,
            };

            level.pickableTypeRecords.Add(record);
            level.pickableTypeLookup.Add(type, record);
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Pickable) return;

        if (eventContext.pickableType is not PickableType type) return;

        if (eventContext.pickableAction is not PickableAction action) return;

        if (level.pickableTypeLookup.TryGetValue(type, out PickableTypeRecord record))
        {
            switch (action)
            {
                case PickableAction.Collected:
                    level.totalTimePickedObject++;
                    record.collected++;
                    break;
                case PickableAction.Dropped:
                    record.dropped++;
                    break;
                case PickableAction.PlacedInSlot:
                    record.placedInSlot++;
                    break;

            }
        }
    }
}
