using System;
using System.IO;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [Header("Events")]
    [SerializeField] private SaveDataEventSO _saveDataEvent;
    [SerializeField] private LoadDataEventSO _loadDataEvent;

    private string saveFolderLocation;

    protected override void Awake()
    {
        base.Awake();
        saveFolderLocation = Path.Combine(Application.persistentDataPath, "saves");
    }

    private void OnEnable()
    {
        _saveDataEvent.OnSave += SaveData;
        _loadDataEvent.OnLoad += LoadData;
    }

    private void OnDisable()
    {
        _saveDataEvent.OnSave -= SaveData;
        _loadDataEvent.OnLoad -= LoadData;
    }

    private void EnsureFolderExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    private void SaveData(ISavableData data) // Called by event
    {
        string folderPath = Path.Combine(saveFolderLocation, data.SaveSubFolder);
        EnsureFolderExist(folderPath);

        string fullPath = Path.Combine(folderPath, $"{data.SaveFileName}.json");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Data saved to: {fullPath}");
    }

    private ISavableData LoadData(string fileName, string subFolder, Type type) // Called by event
    {
        string folderPath = Path.Combine(saveFolderLocation, subFolder);
        string fullPath = Path.Combine(folderPath, $"{fileName}.json");

        if (!File.Exists(fullPath))
        {
            Debug.LogWarning("No path found!");
            return null;
        }

        string json = File.ReadAllText(fullPath);
        return JsonUtility.FromJson(json, type) as ISavableData;
    }
}
