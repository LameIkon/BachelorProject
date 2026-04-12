using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataHandler : IDisposable
{
    private readonly RegisterSaveDataEventSO _registerDataEvent;
    private readonly StoreDataEventSO _storeDataEvent;

    private SessionRecord _sessionRecords;

    // Data trackers for storing data
    private readonly PickableDataTracker _pickableTracker;
    private readonly TerminalDataTracker _terminalTracker;

    private LevelRecord _currentLevel;

    public DataHandler(RegisterSaveDataEventSO registerSaveData, StoreDataEventSO getData)
    {
        _registerDataEvent = registerSaveData;
        _storeDataEvent = getData;
 


        //_pickableTracker = new PickableDataTracker();
        _terminalTracker = new TerminalDataTracker();

        // Date or session as name
        DateTime dateTime = DateTime.Now;
        Debug.Log(dateTime.ToString("F"));

        _sessionRecords = new SessionRecord
        {
            sessionName = dateTime.ToString("dd-MM-yyyy_HH.mm.ss"), // Formatting for folder text 
            sessionData = dateTime.ToString("F"),
            levelRecords = new List<LevelRecord>()
        };


        _storeDataEvent.OnRaise += StoreData;
    }

    #region Data storing
    private void StoreData(InteractionEvent context)
    {
        EventType eventType = context.eventType;
        switch (eventType)
        {
            case EventType.Button:
                break;
            case EventType.Terminal:
                if (context.terminalState is TerminalState state)
                {
                    _terminalTracker.Add(state);
                }
                break;
        }
    }

    public void TrackLevel(string levelName, bool shouldTrack)
    {
        if (!shouldTrack) return; // Currently redundant. we want to check what levels we even want to track data on

        string uniqueName = GenerateUniqueName(levelName, _sessionRecords.levelRecords.Select(record => record.name));

        LevelRecord newLevelRecord = new LevelRecord()
        {
            name = uniqueName
        };  


        _sessionRecords.levelRecords.Add(newLevelRecord);
        _currentLevel = newLevelRecord;
    }

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

    #endregion

    #region Data Fetching
    public void SaveTrackedData()
    {
        Debug.Log("Save data");

        // Organize
        OrganizeData();

        // Save levels
        foreach (LevelSaveData levelData in SaveLevel())
        {
            Save(levelData);
        }

        // Save session
        Save(SaveSession());

        // Reset Trackers
        ResetTrackers();
    }

    private IEnumerable<LevelSaveData> SaveLevel()
    {
        // Save levels
        foreach (LevelRecord levelRecord in _sessionRecords.levelRecords)
        {
            yield return new LevelSaveData
            {
                sessionInstance = _sessionRecords.sessionName,
                levelName = levelRecord.name,
                levelRecord = levelRecord
            };
        }
    }
    private SessionSaveData SaveSession()
    {
        //public List<TerminalStateRecord> terminalStateRecords = new();
        SessionSaveData sessionSaveData = new SessionSaveData()
        {
            sessionInstanceName = _sessionRecords.sessionName,

        };
        return sessionSaveData;
    }

    private void Save(ISavableData data)
    {
        if (data == null) return;
        _registerDataEvent.Save(data);
    }

    private void OrganizeData()
    {
        _currentLevel?.terminalStateRecords.AddRange(_terminalTracker.GetRecords());
    }

    #endregion

    #region Clean up
    private void ResetTrackers()
    {
        _terminalTracker.Reset();
    }

    public void Dispose()
    {
        _storeDataEvent.OnRaise -= StoreData;
    }
    #endregion

    #region Trackers

    public abstract class BaseDataTracker
    {
        public abstract void Initialize();

        public abstract void Add();

        //public abstract IEnumerable<> GetRecords();

        public abstract void Reset();
    }

    protected class TerminalDataTracker
    {
        private readonly Dictionary<TerminalState, TerminalStateRecord> _terminalRecords;

        public TerminalDataTracker()
        {
            _terminalRecords = new Dictionary<TerminalState, TerminalStateRecord>();
            Initialize();
        }

        private void Initialize()
        {
            foreach (TerminalState state in Enum.GetValues(typeof(TerminalState)))
            {
                _terminalRecords[state] = new TerminalStateRecord
                {
                    state = state.ToString(),
                    count = 0
                };
            }
        }

        /// <summary>
        /// Increase the counter by one for the specified terminal state
        /// </summary>
        /// <param name="state"></param>
        public void Add(TerminalState state)
        {
            Debug.Log($"Add {state}");
            _terminalRecords[state].count++;
        }

        /// <summary>
        /// Returns a list of all terminalStateRecords
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TerminalStateRecord> GetRecords()
        {
            return _terminalRecords.Values;
        }

        /// <summary>
        /// Resets the counter inside the dictionary 
        /// </summary>
        public void Reset() 
        {
            foreach (TerminalStateRecord record in _terminalRecords.Values)
            {
                record.count = 0;
            }
        }
    }


    protected class PickableDataTracker
    {
        private Dictionary<ButtonType, ButtonRecord> _records = new();

        public void Add(ButtonType type, InteractionEvent context)
        {
            if (!_records.ContainsKey(type)) _records[type] = new ButtonRecord
            {
                type = type
            };

            //context.buttonType

            switch (context.eventType)
            {
                case EventType.Pickable:

                    break;
            }
        }
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

# region Organized data Ready to Save

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
    public List<Quest> quests = new();

    // Terminal states
    public List<TerminalStateRecord> terminalStateRecords = new();

    // Buttons
    public List<ButtonRecord> buttonRecords = new();

    // Pickable items
    public List<PickableTypeRecord> pickableTypeRecords = new();

}

#endregion

# region Storing unsaved data methods

[Serializable]
public class SessionRecord
{
    public string sessionName;
    public string sessionData;
    // TBD add more data here for whole session
    public List<LevelRecord> levelRecords; // Each individual levels
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

# region DataTracking methods
[Serializable]
public class TerminalStateRecord
{
    public string state; 
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