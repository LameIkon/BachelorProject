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
    private readonly List<LevelRecordBuilderSO> _recordBuilders;

    private SessionRecord _sessionRecord;
    private LevelRecord _currentLevel;

    // timing
    private float _sessionStartTime;

    private bool _isCapturing = true; // Decides if we can track data or not 


    public DataHandler(RegisterSaveDataEventSO registerSaveData, StoreDataEventSO getData, List<LevelRecordBuilderSO> recordBuilders)
    {

        _registerDataEvent = registerSaveData;
        _storeDataEvent = getData;
        _recordBuilders = recordBuilders;

        _storeDataEvent.InitializeTimeProvider(SessionTime);
 
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

        foreach (LevelRecordBuilderSO builder in _recordBuilders)
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

        foreach (LevelRecordBuilderSO builder in _recordBuilders)
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
        SessionSaveData session = new SessionSaveData()
        {
            sessionInstanceName = _sessionRecord.sessionName,
            totalTime = SessionTime()

        };

        foreach (LevelRecord level in _sessionRecord.levelRecords)
        {
            // Terminal
            session.totalTerminalOff += level.terminalStateRecords.Where(value => value.state == TerminalState.Off.ToString()).Sum(value => value.count);
            session.totalTerminalRunning += level.terminalStateRecords.Where(value => value.state == TerminalState.Running.ToString()).Sum(value => value.count);
            session.totalTerminalWarning += level.terminalStateRecords.Where(value => value.state == TerminalState.Warning.ToString()).Sum(value => value.count);
            session.totalTerminalLeverWarning += level.terminalStateRecords.Where(value => value.state == TerminalState.LeverWarning.ToString()).Sum(value => value.count);

            // Pickables
            session.totalObjectsCollected += level.pickableTypeRecords.Sum(value => value.collected);
            session.totalObjectsDropped += level.pickableTypeRecords.Sum(value => value.dropped);
            session.totalObjectsPlaced += level.pickableTypeRecords.Sum(value => value.placedInSlot);

            // Buttons
        }

        return session;
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

    // Pickables
    public int totalObjectsCollected;
    public int totalObjectsDropped;
    public int totalObjectsPlaced;

    // Buttons
    public int totalButtonsPressed;
    public int totalButtonsSuccessfullPress;
    public int totalButtonsUnsuccessfullPress;
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

    /// <summary>
    /// The more specific data to be tracked. Each list will have a corresponding dictionary for faster lookup
    /// </summary>

    // Quest
    public List<QuestRecord> questRecords;

    public Dictionary<Quest, QuestRecord> questLookup;
    public Dictionary<QuestPart, QuestPartRecord> questPartLookup;

    // Terminal states
    public List<TerminalStateRecord> terminalStateRecords;

    public Dictionary<TerminalState, TerminalStateRecord> terminalStateLookup;

    // Buttons
    public int totalButtonSuccess;
    public int totalButtonUnsuccess;
    public List<ButtonRecord> buttonRecords;

    public Dictionary<ButtonType, ButtonRecord> buttonLookup;

    // Pickable items
    public int totalTimePickedObject;
    public List<PickableTypeRecord> pickableTypeRecords;

    public Dictionary<PickableType, PickableTypeRecord> pickableTypeLookup;

    // Compendium
    public int totalTimeOpenCompendium;
    public List<CompendiumOpenRecord> compendiumOpenRecords;
    public List<CompendiumPageRecord> compendiumPageRecords;

    public Dictionary<CompendiumOpenMethod, CompendiumOpenRecord> compendiumOpenLookup;
    public Dictionary<CompendiumID, CompendiumPageRecord> compendiumPageLookup;

}

#endregion

# region Get Data Events

[Serializable]
public struct InteractionEvent
{
    public EventType eventType; // type to categorize it

    // specific event types. Only one should be used

    // Time
    public float timeStamp;

    // Pickable
    public PickableType? pickableType;
    public PickableAction? pickableAction;

    // Button
    public ButtonType? buttonType;
    public ButtonOutcome? buttonAction;

    // Terminal
    public TerminalState? terminalState;

    // Compendium
    public CompendiumOpenMethod? compendiumOutcome;
    public CompendiumID? compendiumID;

    // Quest
    public Quest quest;
    public QuestPart questPart;
    public QuestEventType? questEventType;
}

public enum EventType : byte
{
    Pickable,
    Button,
    Terminal,
    Compendium,
    Quest
}

public enum TerminalState : byte
{
    Off,
    Running,
    Warning,
    LeverWarning
}

public enum PickableAction : byte
{
    Collected,
    Dropped,
    PlacedInSlot
}

/// <summary>
/// Weather you succesfully press a button and it works or not
/// </summary>
public enum ButtonOutcome : byte
{
    Success,
    Fail,
}

public enum CompendiumOpenMethod : byte
{
    InteractionMenu,
    KeyToggle
}

public enum QuestEventType : byte
{
    Started,
    PartCompleted,
    Completed
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
    public string type; // PicktableType
    public int collected;
    public int dropped;
    public int placedInSlot;
}

[Serializable]
public class ButtonRecord
{
    public string type; // ButtonType
    public int pressed;
    public int succes;
    public int failed;
}

[Serializable]
public class QuestRecord
{
    public Quest quest;
    public List<QuestPartRecord> questParts;
    public float timeStarted;
    public float timeFinished;
    public float timeDuration;
}

[Serializable]
public class QuestPartRecord
{
    public QuestPart part;
    public float timeStarted;
    public float timeFinished;
    public float timeDuration;
}

[Serializable]
public class CompendiumOpenRecord
{
    public string openMethod; // CompendiumOpenWith
    public int count;
}

[Serializable]
public class CompendiumPageRecord
{
    public string pageID; // What you open up to
    public int count;
}


#endregion