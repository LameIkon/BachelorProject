using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Terminal Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Terminal Record")]
public class TerminalRecordBuilderSO : EventRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        level.terminalStateRecords = new List<TerminalStateRecord>();

        foreach (TerminalState state in Enum.GetValues(typeof(TerminalState)))
        {
            level.terminalStateRecords.Add(new TerminalStateRecord
            {
                state = state.ToString(),
                count = 0
            });
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Terminal || !eventContext.terminalState.HasValue) return; // If it is not terminal or state has no value assigned return

        TerminalState state = eventContext.terminalState.Value;

        foreach (TerminalStateRecord terminalRecord in level.terminalStateRecords)
        {
            if (terminalRecord.state.ToString() == state.ToString())
            {
                terminalRecord.count++;
                break;
            }
        }
    }
}