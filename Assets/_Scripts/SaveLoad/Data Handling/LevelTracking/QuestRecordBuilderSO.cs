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
        level.questPartLookup = new Dictionary<QuestPart, QuestPartRecord>();
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Quest) return;

        if (eventContext.quest is not Quest quest) return;

        if (eventContext.questEventType is not QuestEventType questEvent) return;

        float time = eventContext.timeStamp;

        // Ensure quest record exists
        if (!level.questLookup.TryGetValue(quest, out QuestRecord questRecord))
        {
            questRecord = new QuestRecord
            {
                quest = quest,
                timeStarted = time,
                questParts = new List<QuestPartRecord>()

            };

            level.questRecords.Add(questRecord);
            level.questLookup.Add(quest, questRecord);
        }

        // Quest event
        switch (questEvent)
        {
            case QuestEventType.Completed:
                questRecord.timeFinished = time;
                questRecord.timeDuration = questRecord.timeFinished - questRecord.timeStarted;
                break;
        }


        // Part record
        if (eventContext.questPart is not QuestPart questPart) return;

        // Ensure part record exists
        if (!level.questPartLookup.TryGetValue(questPart, out QuestPartRecord partRecord))
        {
            partRecord = new QuestPartRecord
            {
                part = questPart,
                timeStarted = time
            };

            questRecord.questParts.Add(partRecord);
            level.questPartLookup.Add(questPart, partRecord);
        }

        // Quest part event
        switch (questEvent)
        {
            case QuestEventType.PartCompleted:
                partRecord.timeFinished = time;
                partRecord.timeDuration = partRecord.timeFinished - partRecord.timeStarted;
                break;          
        }
    }
}