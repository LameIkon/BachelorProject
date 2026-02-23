using System;
using UnityEngine;

public class savetest1 : MonoBehaviour
{

    [SerializeField] private SaveDataEventSO saveDataEventSO;
    [SerializeField] private LoadDataEventSO loadDataEventSO;

    private void Start()
    {
        Settings settings = new Settings();
        MetaData metaData = new MetaData();
        saveDataEventSO.Save(settings);
        saveDataEventSO.Save(metaData);

        var loadedSaves = loadDataEventSO.Load<Settings>("Saves", "Settings");
        var loadedSaves1 = loadDataEventSO.Load<MetaData>("Data", "Settings");

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
