using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private MouseHandler _mouseHandler;
    private AsyncSceneLoader _sceneLoader;


    [SerializeField] private LevelData[] _levels;
    [SerializeField] private SceneLoadEventSO _sceneLoadEventSO;
    [SerializeField] private QuestGiveEventSO _questGiveEventSO;

    [SerializeField] private LevelData _firstSceneToLoad;

    [SerializeField] private DataHandling _dataHandling;

    #region Unity Method 
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _mouseHandler = new MouseHandler();
        _sceneLoader = new AsyncSceneLoader();
        _dataHandling.Initialize();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadLevel(_firstSceneToLoad);
    }

    private void OnEnable()
    {
        _sceneLoadEventSO.OnRaise += LoadLevel;
    }

    private void OnDisable()
    {
        _sceneLoadEventSO.OnRaise -= LoadLevel;
    }

    #endregion

    private void OnApplicationQuit()
    {
        _dataHandling.DataHandler.SaveData();
    }


    private void LoadLevel(LevelData levelData) 
    {
        StartCoroutine(LoadLevelCoroutine(levelData));
    }

    private IEnumerator LoadLevelCoroutine(LevelData levelData) 
    {
        yield return StartCoroutine(_sceneLoader.UnloadScenes());

        yield return StartCoroutine(_sceneLoader.LoadScenes(levelData));

        // TODO: Send Quest data through the QuestGiveEventSO, would like to refactor this so it
        // uses the LevelData as a parameter instead of the levelId.
        yield return null;
        _dataHandling.DataHandler.CompleteLevel(); // Store data from current level before switching to new level
        _questGiveEventSO.Raise(levelData.LevelQuest);
        _dataHandling.DataHandler.TrackLevel(levelData.name); // Start tracking new level
    }


    /// <summary>
    /// Create new session whenever we are done with the previous
    /// </summary>
    private void NewSession()
    {
        _dataHandling.DataHandler.SaveData();
        _dataHandling.DataHandler.Dispose();
        _dataHandling.Initialize();

    }


    [Serializable]
    protected class DataHandling
    {
        [SerializeField] private RegisterSaveDataEventSO _registerSaveDataEvent;
        [SerializeField] private StoreDataEventSO _getDataEvent;
        [SerializeField] private List<EventRecordBuilderSO> _recordBuilders;


        private DataHandler _dataHandler;

        public DataHandler DataHandler => _dataHandler;
        
        public void Initialize()
        {
            if (_registerSaveDataEvent == null || _getDataEvent == null) return;
            _dataHandler = new DataHandler(_registerSaveDataEvent, _getDataEvent, _recordBuilders);
        }
    }

}


