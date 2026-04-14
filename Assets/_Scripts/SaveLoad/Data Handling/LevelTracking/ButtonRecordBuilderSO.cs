using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Button Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Button Record")]
public class ButtonRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.buttonRecords = new List<ButtonRecord>();

        // For more efficient lookup
        level.buttonLookup = new Dictionary<ButtonType, ButtonRecord>();

        foreach (ButtonType type in Enum.GetValues(typeof(ButtonType)))
        {
            ButtonRecord record = new ButtonRecord()
            {
                type = type.ToString(),
                pressed = 0,
                succes = 0,
                failed = 0,
            };

            level.buttonRecords.Add(record);
            level.buttonLookup.Add(type, record);
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Button) return;

        if (eventContext.buttonType is not ButtonType type) return;

        if (eventContext.buttonAction is not ButtonOutcome action) return;

        // If we have both Type and Outcome for button we can then be able to look up
        if (level.buttonLookup.TryGetValue(type, out ButtonRecord record))
        {
            record.pressed++; // Always imcrement when pressed

            switch (action)
            {
                case ButtonOutcome.Success:
                    level.totalButtonSuccess++;
                    record.succes++;
                    break;
                case ButtonOutcome.Fail:
                    level.totalButtonUnsuccess++;
                    record.failed++;
                    break;
            }
        }      
    }
}
