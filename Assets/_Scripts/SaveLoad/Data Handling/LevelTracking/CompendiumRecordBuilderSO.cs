using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Compendium Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Compendium Record")]
public class CompendiumRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.compendiumPageRecords = new List<CompendiumPageRecord>();

        // For more efficient lookup
        level.compendiumPageLookup = new Dictionary<CompendiumID, CompendiumPageRecord>();

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
