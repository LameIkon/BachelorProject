using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : Singleton<SaveSystem>
{
    [Header("Events")]
    [SerializeField] private SaveAllDataEventSO _saveAllEvent;
    [SerializeField] private RegisterSaveDataEventSO _saveDataEvent;
    [SerializeField] private LoadDataEventSO _loadDataEvent;


    private string _saveFolderLocation;

    private List<ISavableData> _savables = new List<ISavableData>(); 

    protected override void Awake()
    {
        base.Awake();
        _saveFolderLocation = Path.Combine(Application.persistentDataPath, "saves");
        EnsureFolderExist(_saveFolderLocation);
    }

    private void OnEnable()
    {
        _saveAllEvent.OnRaise += SaveAll;
        _saveDataEvent.OnSave += Register;
        _loadDataEvent.OnLoad += LoadData;
    }

    private void OnDisable()
    {
        _saveAllEvent.OnRaise -= SaveAll;
        _saveDataEvent.OnSave -= Register;
        _loadDataEvent.OnLoad -= LoadData;
    }

    #region Registration
    private void Register(ISavableData savable)
    {
        if (!_savables.Contains(savable))
        {
            _savables.Add(savable);
            SaveData(savable); // Save it first time we register
        }
    }

    #endregion

    # region Save method
    private void SaveAll()
    {
        foreach (ISavableData savable in _savables)
        {
            SaveData(savable);
        }
    }

    private void SaveData(ISavableData data) // Called by event
    {

        CreateFoldersAnPath(_saveFolderLocation, data.SaveFolder);           

        string fullPath = Path.Combine(_saveFolderLocation, data.SaveFolder.Name, $"{data.SaveFileName}.json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Data saved to: {fullPath}");
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
    private string CreateFoldersAnPath(string basePath, SaveFolder folder)
    {
        string currentPath = Path.Combine(basePath, folder.Name);
        EnsureFolderExist(currentPath);

        if (folder.Subfolders == null || folder.Subfolders.Count == 0)
        {
            return currentPath;
        }

        return CreateFoldersAnPath(currentPath, folder.Subfolders[0]);
    }

    private void EnsureFolderExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    #endregion
}
