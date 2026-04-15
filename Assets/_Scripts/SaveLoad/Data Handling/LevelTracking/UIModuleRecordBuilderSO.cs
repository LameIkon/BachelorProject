using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UIModule Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/UIModule Record")]
public class UIModuleRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.uiModuleRecords = new List<UIModuleRecord>();

        // For more efficient lookup
        level.uiModuleLookup = new Dictionary<UIType, UIModuleRecord>();


        // For opening
        foreach (UIType type in Enum.GetValues(typeof(UIType)))
        {
            UIModuleRecord record = new UIModuleRecord
            {
                uiType = type.ToString(),
                timesOpenedTotal = 0,
                uiInternalAmount = 0,
                worldButtonAmount = 0,
                hotkeyAmount = 0
            };

            level.uiModuleRecords.Add(record);
            level.uiModuleLookup.Add(type, record);
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.UIModule) return;

        if (eventContext.UIRequest is not UIRequest request) return;

        if (level.uiModuleLookup.TryGetValue(request.type, out UIModuleRecord record))
        {
            record.timesOpenedTotal++;

            switch (request.source)
            {
                case UIInteractionSource.UIInternal:
                    record.uiInternalAmount++;
                    break;
                case UIInteractionSource.WorldButton:
                    record.worldButtonAmount++;
                    break;
                case UIInteractionSource.Hotkey:
                    record.hotkeyAmount++;
                    break;
            }
        }
    }
}
