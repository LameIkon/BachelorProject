using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pickable Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Pickable Record")]
public class PickableRecordBuilderSO : EventRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        level.pickableTypeRecords = new List<PickableTypeRecord>();

        foreach (PickableType type in Enum.GetValues(typeof(PickableType)))
        {
            level.pickableTypeRecords.Add(new PickableTypeRecord
            {
                type = type.ToString(),
                collected = 0,
                dropped = 0,
                placedInSlot = 0,
            });
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Pickable) return;

        if (eventContext.pickableType is not PickableType type) return;

        if (eventContext.pickableAction is not PickableAction action) return;

        foreach (PickableTypeRecord record in level.pickableTypeRecords)
        {
            if (record.type != type.ToString()) continue; // Search until we find the correct record

            switch (action)
            {
                case PickableAction.Collected:
                    record.collected++;
                    break;
                case PickableAction.Dropped:
                    record.dropped++;
                    break;
                case PickableAction.PlacedInSlot:
                    record.placedInSlot++;
                    break;

            }
            break; // Break once we find the matching record
        }
    }
}
