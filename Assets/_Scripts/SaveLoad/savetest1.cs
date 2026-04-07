using System;
using System.Collections.Generic;
using UnityEngine;

public class savetest1 : MonoBehaviour
{
    [SerializeField] private RegisterSaveDataEventSO registerSaveData;
    [SerializeField] private LoadDataEventSO loadData;

    private void Start()
    {
        SessionOverallData sessionOverallData = new SessionOverallData()
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
            levelRecord = new OrganizedLevelRecord
            {
                levelStarted = 3f,
                levelFinished = 10f,
                levelDuration = 7f
            }
        };

        registerSaveData.Save(sessionOverallData);
        registerSaveData.Save(levelSaveData);

    }

    private void SaveTestPrototype()
    {
        //Settings settings = new Settings();
        //MetaData metaData = new MetaData();
        //registerSaveData.Save(settings);
        //registerSaveData.Save(metaData);

        //var loadedSaves = loadData.Load<Settings>("Saves", "Settings");
        //var loadedSaves1 = loadData.Load<MetaData>("Data", "Settings");

        //Debug.Log(loadedSaves.Time);
        //Debug.Log(loadedSaves1.Time);
    }
}


[Serializable]
public class Settings : ISavableData
{
    public string SaveFileName => "Saves";

    public string SaveSubFolder => "Settings";

    public List<string> SaveFolders => throw new NotImplementedException();

    public SaveFolder SaveFolder => throw new NotImplementedException();

    public List<string> SavePath => throw new NotImplementedException();

    public float Time = 23f;
}

[Serializable]
public class MetaData : ISavableData
{
    public string SaveFileName => "Data";

    public string SaveSubFolder => "Settings";

    public List<string> SaveFolders => throw new NotImplementedException();

    public SaveFolder SaveFolder => throw new NotImplementedException();

    public List<string> SavePath => throw new NotImplementedException();

    public string Time = "some time here";
}
