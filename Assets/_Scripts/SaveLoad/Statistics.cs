using System;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private RegisterSaveDataEventSO registerSaveData;
    [SerializeField] private LoadDataEventSO loadData;

    private GameSessionRecord _gameSessionRecords;
    private OrganizedGameSessionRecord _session;

    /// <summary>
    /// Each time a level gets started, create a record for the level to track data
    /// </summary>
    /// <param name="levelData">The level started</param>
    public void StoreLevelData(LevelData levelData)
    {
        LevelRecord levelRecord = new LevelRecord
        {
            levelName = levelData.name,
            levelStarted = Time.time,
            entries = new List<InteractionEvent>()
        };


        _gameSessionRecords.levelRecords.Add(levelRecord);
    }

    /// <summary>
    /// Create session data when game starts
    /// </summary>
    /// <param name="name">name of the session</param>
    public void CreateSessionData(string name)
    {
        _gameSessionRecords = new GameSessionRecord
        {
            sessionName = name,
            levelRecords = new List<LevelRecord>()
        };
    }


    private void CategorizeData()
    {
        _session = new();

        // Sum total time across all levels
        _session.totalTime = 0;

        foreach (LevelRecord level in _gameSessionRecords.levelRecords)
        {
            OrganizedLevelRecord organized = new();
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
            _session.levelRecords.Add(organized);
        }

        // Save after categorizing
        SaveSession();
    }

    private void SaveSession()
    {       
        registerSaveData.Save(_session);
    }

}

[Serializable]
public class OrganizedGameSessionRecord : ISavableData
{
    public string SaveFileName => "SessionData";

    List<string> ISavableData.SaveFolders => throw new NotImplementedException();

    // Session folder
    public string session; // Name of session. Child of Saves

    // Subfolders
    public string overall = "Overall"; // Child of session
    public string levels = "Levels"; // Child of session

    public List<string> SaveFolders(string targetFolder)
    {
        return new List<string>
        {
            overall,
            levels
        };
    }


    public float totalTime;
    public List<OrganizedLevelRecord> levelRecords = new();
}

[Serializable]
public class OrganizedLevelRecord
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



[Serializable]
public class GameSessionRecord
{
    public string sessionName;
    public List<LevelRecord> levelRecords;
}

[Serializable]
public class LevelRecord
{
    public string levelName;
    public float levelStarted;
    public float levelFinished;
    public float levelDuration;
    public List<InteractionEvent> entries;
}



[Serializable]
public class InteractionEvent
{
    public float time; // Time since level start
    public EventType eventType; // type to categorize it

    // specific event types. Only one should be used
    public PickableType? pickableType;
    public ButtonType? buttonType;
    public TerminalState? TerminalState;
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
