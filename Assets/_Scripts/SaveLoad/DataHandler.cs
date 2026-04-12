using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataHandler : IDisposable
{
    // Events
    private readonly RegisterSaveDataEventSO _registerDataEvent;
    private readonly StoreDataEventSO _storeDataEvent;

    // Data Builders
    private readonly List<EventRecordBuilderSO> _recordBuilders;

    private SessionRecord _sessionRecord;
    private LevelRecord _currentLevel;

    // timing
    private float _sessionStartTime;

    private bool _isCapturing = true; // Decides if we can track data or not 


    public DataHandler(RegisterSaveDataEventSO registerSaveData, StoreDataEventSO getData, List<EventRecordBuilderSO> recordBuilders)
    {
        _registerDataEvent = registerSaveData;
        _storeDataEvent = getData;
        _recordBuilders = recordBuilders;
 
        _storeDataEvent.OnRaise += StoreData;


        StartNewSession();
    }

    private void StartNewSession()
    {
        _sessionStartTime = Time.realtimeSinceStartup;

        // Date or session as name
        DateTime dateTime = DateTime.Now;

        _sessionRecord = new SessionRecord
        {
            sessionName = dateTime.ToString("dd-MM-yyyy_HH.mm.ss"), // Formatting for folder text 
            sessionData = dateTime.ToString("F"),
            levelRecords = new List<LevelRecord>()
        };
    }


    #region Data storing
    /// <summary>
    /// Store all data that happens in the game to session and level
    /// </summary>
    /// <param name="context"></param>
    private void StoreData(InteractionEvent context)
    {
        if (!_isCapturing) return;

        foreach (EventRecordBuilderSO builder in _recordBuilders)
        {
            builder.Apply(_currentLevel, context);
        }
    }

    public void TrackLevel(string levelName, bool shouldTrack = true)
    {
        if (!shouldTrack) return; // Currently redundant. we want to check what levels we even want to track data on. No reason to track MainMenu

        string uniqueName = GenerateUniqueName(levelName, _sessionRecord.levelRecords.Select(record => record.name));

        _currentLevel = new LevelRecord()
        {
            name = uniqueName,
            levelStarted = SessionTime()
        };  

        foreach (EventRecordBuilderSO builder in _recordBuilders)
        {
            builder.Initialize(_currentLevel);
        }

        _sessionRecord.levelRecords.Add(_currentLevel);
    }
    #endregion

    #region Level lifeCycle
    public void CompleteLevel()
    {
        if (_currentLevel == null) return;

        // Finalize Timing
        _currentLevel.levelFinished = SessionTime();
        _currentLevel.levelDuration = _currentLevel.levelFinished - _currentLevel.levelStarted;

        _currentLevel = null;

    }
    #endregion


    #region Saving
    public void SaveData()
    {
        Debug.Log("Save data");

        CompleteLevel();

        // Save levels
        foreach (LevelSaveData levelData in SaveLevel())
        {
            Save(levelData);
        }

        // Save session
        Save(SaveSession());
    }

    private IEnumerable<LevelSaveData> SaveLevel()
    {
        foreach (LevelRecord levelRecord in _sessionRecord.levelRecords)
        {
            yield return new LevelSaveData
            {
                sessionInstance = _sessionRecord.sessionName,
                levelName = levelRecord.name,
                levelRecord = levelRecord
            };
        }
    }
    private SessionSaveData SaveSession()
    {
        SessionSaveData sessionSaveData = new SessionSaveData()
        {
            sessionInstanceName = _sessionRecord.sessionName,

        };

        foreach (LevelRecord levelRecord in _sessionRecord.levelRecords)
        {
            // Terminal Data
            foreach (TerminalStateRecord record in levelRecord.terminalStateRecords)
            {
               if (!Enum.TryParse(record.state, out TerminalState state)) continue;

                switch (state)
                {
                    case TerminalState.Off:
                        sessionSaveData.totalTerminalOff += record.count;
                        break;
                    case TerminalState.Running:
                        sessionSaveData.totalTerminalRunning += record.count;
                        break;
                    case TerminalState.Warning:
                        sessionSaveData.totalTerminalWarning += record.count;
                        break;
                    case TerminalState.LeverWarning:
                        sessionSaveData.totalTerminalLeverWarning += record.count;
                        break;

                }
            }
        }

        return sessionSaveData;
    }

    private void Save(ISavableData data)
    {
        if (data == null) return;
        _registerDataEvent.Save(data);
    }
    #endregion




    #region Clean up

    public void Dispose()
    {
        _storeDataEvent.OnRaise -= StoreData;
    }
    #endregion

    #region Utilities
    private string GenerateUniqueName(string name, IEnumerable<string> existingNames)
    {
        string uniqueName = name;
        int counter = 1;

        while (existingNames.Contains(uniqueName)) // If any by that name exist 
        {
            // Add the counter to the name and loop again to check
            uniqueName = $"{name}_{counter}"; 
            counter++;
        }
        return uniqueName;
    }

    private float SessionTime()
    {
        return Time.realtimeSinceStartup - _sessionStartTime;
    }

    public void SetCapture(bool value)
    {
        _isCapturing = value;
    }
    #endregion
}

#region ISavable 

/// <summary>
/// Data combined from levelSaveData to create a more compact structure of whole session.
/// Needs a uniq name for session instance
/// </summary>
[Serializable]
public struct SessionSaveData : ISavableData
{
    // Folder Initialization
    public string sessionInstanceName; // Name of current session
    public string SaveFileName => "SessionData";

    public List<string> SavePath => new List<string>()
    {
        "Session",
        sessionInstanceName
    };

    // Time
    public float totalTime;


    // Compendium
    public int totalCompendiumOpenedWithHotkey;
    public int totalCompendiumOpenedWithMenu;

    // Player
    public float totalDistanceMoved;

    // Terminals
    public int totalTerminalOff;
    public int totalTerminalRunning;
    public int totalTerminalWarning;
    public int totalTerminalLeverWarning;
}

/// <summary>
/// Need a name on the session instance the level entries should populate. Require sessionInstance and levelName and levelRecord
/// </summary>
[Serializable]
public struct LevelSaveData : ISavableData
{
    // Folder Initialization
    public string sessionInstance; // Name of current session
    public string levelName;
    public string SaveFileName => levelName;

    public List<string> SavePath => new List<string>()
    {
        "Session",
        sessionInstance,
        "Levels"
    };

    // Data population
    public LevelRecord levelRecord;
}

#endregion

# region Organized data

[Serializable]
public class SessionRecord
{
    public string sessionName;
    public string sessionData;
    // TBD add more data here for whole session
    public List<LevelRecord> levelRecords; // Each individual levels
}

[Serializable]
public class LevelRecord
{
    // Level Name
    public string name;

    // Level Time
    public float levelStarted;
    public float levelFinished;
    public float levelDuration;

    // Compendium
    public int compendiumOpenedWithHotkey;
    public int compendiumOpenedWithMenu;

    // Player
    public float distanceMoved;

    // Quest
    public List<Quest> quests;

    // Terminal states
    public List<TerminalStateRecord> terminalStateRecords;

    // Buttons
    public List<ButtonRecord> buttonRecords;

    // Pickable items
    public List<PickableTypeRecord> pickableTypeRecords;

}

#endregion

# region Get Data Events

[Serializable]
public struct InteractionEvent
{
    public float? time; // Time since level start
    public EventType eventType; // type to categorize it

    // specific event types. Only one should be used
    public PickableType? pickableType;
    public ButtonType? buttonType;
    public TerminalState? terminalState;
}

public enum EventType
{
    Pickable,
    Button,
    Terminal,
    Compendium,
}

public enum TerminalState
{
    Off,
    Running,
    Warning,
    LeverWarning
}

#endregion

# region Data Tracking methods
[Serializable]
public class TerminalStateRecord
{
    public string state; // TerminalState 
    public int count;
}


[Serializable]
public class PickableTypeRecord
{
    public PickableType type;
    public int collected;
    public int dropped;
    public int placedInSlot;
}

[Serializable]
public class ButtonRecord
{
    public ButtonType type;
    public int pressed;
    public int succes;
    public int failed;
}

[Serializable]
public class QuesRecord
{
    public List<Part> questParts;
    public float time;
}

[Serializable]
public class QuestPartRecord
{
    public Part part;
    public float time;
}

#endregion