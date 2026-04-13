using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Button Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Button Record")]
public class ButtonRecordBuilderSO : EventRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        level.buttonRecords = new List<ButtonRecord>();

        foreach (ButtonType type in Enum.GetValues(typeof(ButtonType)))
        {
            level.buttonRecords.Add(new ButtonRecord
            {
                type = type.ToString(),
                pressed = 0,
                succes = 0,
                failed = 0,
            });
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Button) return;

        if (eventContext.buttonType is not ButtonType type) return;

        if (eventContext.buttonAction is not ButtonOutcome action) return;

        foreach (ButtonRecord record in level.buttonRecords)
        {
            if (record.type != type.ToString()) continue; // Search until we find the correct record

            record.pressed++; // Always imcrement when pressed

            switch (action)
            {
                case ButtonOutcome.Success:
                    record.succes++;
                    break;
                case ButtonOutcome.Fail:
                    record.failed++;
                    break;

            }
            break; // Break once we find the matching record
        }
    }
}