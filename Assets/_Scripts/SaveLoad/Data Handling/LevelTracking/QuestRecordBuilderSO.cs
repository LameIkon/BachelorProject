using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Quest Record")]
public class QuestRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.questRecords = new List<QuestRecord>();

        // For more efficient lookup
        level.questLookup = new Dictionary<Quest, QuestRecord>();

        //// For initialize and tracking
        //foreach (QuestRecord quest in level.questRecords) // We need a list of all quests in game
        //{
        //    QuestRecord record = new QuestRecord
        //    {
        //        quest = null,
        //        questParts = quest.questParts,
        //    };

        //    level.questRecords.Add(record);
        //    level.questLookup.Add(quest, record);
        //}
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Quest) return;

        if (eventContext.compendiumOutcome is CompendiumOpenMethod method)
        {
            level.totalTimeOpenCompendium++; // Always increase when opening

            if (level.compendiumOpenLookup.TryGetValue(method, out CompendiumOpenRecord record))
            {
                record.count++;
            }
        }
    }
}