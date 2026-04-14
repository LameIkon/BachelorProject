using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Terminal Record SO", menuName = "ScriptableObject/SaveLoad/Record analytics/Terminal Record")]
public class TerminalRecordBuilderSO : LevelRecordBuilderSO
{
    public override void Initialize(LevelRecord level)
    {
        // Data to be saved
        level.terminalStateRecords = new List<TerminalStateRecord>();

        // For more efficient lookup
        level.terminalStateLookup = new Dictionary<TerminalState, TerminalStateRecord>();

        foreach (TerminalState state in Enum.GetValues(typeof(TerminalState)))
        {
            TerminalStateRecord record = new TerminalStateRecord()
            {
                state = state.ToString(),
                count = 0
            };

            level.terminalStateRecords.Add(record);
            level.terminalStateLookup.Add(state, record);
        }
    }

    public override void Apply(LevelRecord level, InteractionEvent eventContext)
    {
        if (eventContext.eventType != EventType.Terminal) return; // If it is not terminal or state has no value assigned return

        if (eventContext.terminalState is not TerminalState state) return;
        
        if (level.terminalStateLookup.TryGetValue(state, out TerminalStateRecord record))
        {
            record.count++;
        }
    }
}
