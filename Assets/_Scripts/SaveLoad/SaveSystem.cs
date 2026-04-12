using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : Singleton<SaveSystem>
{
    [Header("Events")]
    [SerializeField] private RegisterSaveDataEventSO _registerSaveDataEvent;
    [SerializeField] private LoadDataEventSO _loadDataEvent;

    [Header("Options")]
    [SerializeField] private bool _notifySaving;

    private string _saveFolderLocation;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _saveFolderLocation = Path.Combine(Application.persistentDataPath, "saves");
        EnsureFolderExist(_saveFolderLocation);
    }

    private void OnEnable()
    {
        _registerSaveDataEvent.OnSave += SaveData;
        _loadDataEvent.OnLoad += LoadData;
    }

    private void OnDisable()
    {
        _registerSaveDataEvent.OnSave -= SaveData;
        _loadDataEvent.OnLoad -= LoadData;
    }

    # region Save method

    private void SaveData(ISavableData data) // Called by event
    {
        string folderPath = _saveFolderLocation;

        foreach (string folder in data.SavePath) // Look for all folders that needs to exists
        {
            folderPath = Path.Combine(folderPath, folder);
            EnsureFolderExist(folderPath); // Create folder path
        }         

        string fullPath = Path.Combine(folderPath, $"{data.SaveFileName}.json"); // We expect the last folder path created is the location to store data
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, json);

        if (_notifySaving) Debug.Log($"Data saved to: {fullPath}");
    }
    #endregion

    #region Load Method

    private ISavableData LoadData(string fileName, string subFolder, Type type) // Called by event
    {
        string folderPath = Path.Combine(_saveFolderLocation, subFolder);
        string fullPath = Path.Combine(folderPath, $"{fileName}.json");

        if (!File.Exists(fullPath))
        {
            Debug.LogWarning("No path found!");
            return null;
        }

        string json = File.ReadAllText(fullPath);
        return JsonUtility.FromJson(json, type) as ISavableData;
    }
    #endregion

    #region Folder
    private void EnsureFolderExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    #endregion
}
