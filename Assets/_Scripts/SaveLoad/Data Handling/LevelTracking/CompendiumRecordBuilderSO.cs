using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Compendium Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Compendium Record")]
public class CompendiumRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.compendiumOpenRecords = new List<CompendiumOpenRecord>();
        level.compendiumPageRecords = new List<CompendiumPageRecord>();

        // For more efficient lookup
        level.compendiumOpenLookup = new Dictionary<CompendiumOpenMethod, CompendiumOpenRecord>();
        level.compendiumPageLookup = new Dictionary<CompendiumID, CompendiumPageRecord>();


        // For opening
        foreach (CompendiumOpenMethod type in Enum.GetValues(typeof(CompendiumOpenMethod)))
        {
            CompendiumOpenRecord record = new CompendiumOpenRecord
            {
                openMethod = type.ToString(),
                count = 0
            };

            level.compendiumOpenRecords.Add(record);
            level.compendiumOpenLookup.Add(type, record);
        }

        // For tracking page you went to
        foreach (CompendiumID id in Enum.GetValues(typeof(CompendiumID)))
        {
            CompendiumPageRecord record = new CompendiumPageRecord
            {
                pageID = id.ToString(),
                count = 0,
            };

            level.compendiumPageRecords.Add(record);
            level.compendiumPageLookup.Add(id, record);
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Compendium) return;

        // Track open method
        if (eventContext.compendiumOutcome is CompendiumOpenMethod method)
        {
            level.totalTimeOpenCompendium++; // Always increase when opening

            if (level.compendiumOpenLookup.TryGetValue(method, out CompendiumOpenRecord record))
            {
                record.count++;
            }
        }

        // Track page 
        if (eventContext.compendiumID is CompendiumID pageID)
        {
            if (level.compendiumPageLookup.TryGetValue(pageID, out CompendiumPageRecord record))
            {
                record.count++;
            }
        }

    }
}
