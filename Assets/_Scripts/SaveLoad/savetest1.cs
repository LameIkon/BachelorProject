using System;
using UnityEngine;

public class savetest1 : MonoBehaviour
{

    [SerializeField] private RegisterSaveDataEventSO registerSaveData;
    [SerializeField] private LoadDataEventSO loadData;

    private void Start()
    {
        Settings settings = new Settings();
        MetaData metaData = new MetaData();
        registerSaveData.Save(settings);
        registerSaveData.Save(metaData);

        var loadedSaves = loadData.Load<Settings>("Saves", "Settings");
        var loadedSaves1 = loadData.Load<MetaData>("Data", "Settings");

        Debug.Log(loadedSaves.Time);
        Debug.Log(loadedSaves1.Time);
    }
}

[Serializable]
public class Settings : ISavableData
{
    public string SaveFileName => "Saves";

    public string SaveSubFolder => "Settings";

    public float Time = 23f;
}

[Serializable]
public class MetaData : ISavableData
{
    public string SaveFileName => "Data";

    public string SaveSubFolder => "Settings";

    public string Time = "some time here";
}
