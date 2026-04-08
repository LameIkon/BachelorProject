using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler
{
    private readonly RegisterSaveDataEventSO _registerDataEvent;
    private readonly GetDataEventSO _getDataEvent;
    //private readonly SaveAllDataEventSO _saveDataEvent;

    private SessionRecord _gameSessionRecords;
    private SessionSaveData _session;

    private readonly PickableDataTracker _pickableTracker;
    private readonly TerminalDataTracker _terminalTracker;

    // Trackers
    //private Dictionary<ButtonType, ButtonRecord> _buttonRecords;
    //private Dictionary<TerminalState, TerminalStateRecord> _terminalRecords;

    public DataHandler(RegisterSaveDataEventSO registerSaveData, GetDataEventSO getData)
    {
        _registerDataEvent = registerSaveData;
        _getDataEvent = getData;

        //_buttonRecords = new Dictionary<ButtonType, ButtonRecord>();
        //_terminalRecords = new Dictionary<TerminalState, TerminalStateRecord>();

        //_pickableTracker = new PickableDataTracker();
        _terminalTracker = new TerminalDataTracker();

        _getDataEvent.OnRaise += StoreData;

    }

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

    public void CategorizeData()
    {

        SessionSaveData sessionOverallData = new SessionSaveData()
        {
            sessionInstanceName = "THETestSession",
            totalTime = 20f,
            totalCompendiumOpenedWithHotkey = 3,
            totalCompendiumOpenedWithMenu = 12,
            totalDistanceMoved = 123123f,
        };

        LevelSaveData levelSaveData = new LevelSaveData()
        {
            sessionInstance = "THETestSession",
            levelName = "level1",
            levelRecord = new LevelRecord
            {
                levelStarted = 3f,
                levelFinished = 10f,
                levelDuration = 7f
            }
        };


        _session = new();

        // Sum total time across all levels
        _session.totalTime = 0;

        foreach (LevelRecord level in _gameSessionRecords.levelRecords)
        {
            LevelRecord organized = new();
            organized.levelDuration = level.levelDuration; // Set time
            _session.totalTime += level.levelDuration; // Add time to total

            foreach (InteractionEvent entries in level.entries)
            {
                switch (entries.eventType) // TBD
                {
                    default:
                        break;
                }
            }

            // Add level to organized record
            //_session.levelRecords.Add(organized);
        }

        // Save after categorizing
        SaveSession();
    }

    public void SaveSession()
    {       
        _registerDataEvent.Save(_session);
    }

    ///// <summary>
    ///// Each time a level gets started, create a record for the level to track data
    ///// </summary>
    ///// <param name="levelData">The level started</param>
    //private void StoreLevelData(LevelData levelData)
    //{
    //    LevelRecord levelRecord = new LevelRecord
    //    {
    //        levelName = levelData.name,
    //        levelStarted = Time.time,
    //        entries = new List<InteractionEvent>()
    //    };


    //    _gameSessionRecords.levelRecords.Add(levelRecord);
    //}

    /// <summary>
    /// Create session data when game starts
    /// </summary>
    /// <param name="name">name of the session</param>
    //public void CreateSessionData(string name)
    //{
    //    _gameSessionRecords = new GameSessionRecord
    //    {
    //        sessionName = name,
    //        levelRecords = new List<LevelRecord>()
    //    };
    //}

    #region Trackers
    //private void PickableTracker(InteractionEvent context)
    //{

    //}

    //private void TerminalTracker(TerminalState state)
    //{
    //    _terminalRecords.TryAdd(state, new TerminalStateRecord // Initialize it if does not exist 
    //    {
    //        state = state,
    //        count = 0,
    //    });

    //    _terminalRecords[state].count++; // Add to count
    //}

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
                    state = state,
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
public class SessionSaveData : ISavableData
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
public class LevelSaveData : ISavableData
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
    // TBD add more data here for whole session
    public List<LevelRecord> levelRecords; // Each individual levels
}

//[Serializable]
//public class LevelRecord
//{
//    public string levelName;
//    public float levelStarted;
//    public float levelFinished;
//    public float levelDuration;
//    public List<InteractionEvent> entries;
//}

#endregion

# region Get Data Events

[Serializable]
public struct InteractionEvent
{
    public float time; // Time since level start
    public EventType eventType; // type to categorize it

    // specific event types. Only one should be used
    public PickableType? pickableType;
    public ButtonType? buttonType;
    public TerminalState? terminalState;
}

public enum EventType
{
    // Pickable
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
    public TerminalState state;
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