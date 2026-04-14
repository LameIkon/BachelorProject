using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Compendium Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Compendium Record")]
public class CompendiumRecordBuilderSO : EventRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        level.compendiumRecords = new List<CompendiumRecord>();

        foreach (CompendiumOpenWith type in Enum.GetValues(typeof(CompendiumOpenWith)))
        {
            level.compendiumRecords.Add(new CompendiumRecord
            {
                compendiumOpenWith = type.ToString(),
                count = 0,
            });
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Compendium) return;

        if (eventContext.compendiumOutcome is not CompendiumOpenWith action) return;

        foreach (CompendiumRecord record in level.compendiumRecords)
        {
            if (record.compendiumOpenWith == action.ToString())
            {
                record.count++;
                break; // Break once we find the matching record
            }
        }
    }
}