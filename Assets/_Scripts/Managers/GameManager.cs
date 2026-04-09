using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private MouseHandler _crosshairHandler;
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
        _crosshairHandler = new MouseHandler();
        _sceneLoader = new AsyncSceneLoader();
        //_dataHandling.Initialize();
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
        _crosshairHandler.Dispose();
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
        _questGiveEventSO.Raise(levelData.LevelQuest);
    }


    /// <summary>
    /// Create new session whenever we are done with the previous
    /// </summary>
    private void NewSession()
    {
        // TBD. We need to save data first before disposing
        _dataHandling.DataHandler.Dispose();
        _dataHandling.Initialize();

    }

    [Serializable]
    protected class DataHandling
    {
        [SerializeField] private RegisterSaveDataEventSO _registerSaveDataEvent;
        [SerializeField] private GetDataEventSO _getDataEventSO;

        private DataHandler _dataHandler;

        public DataHandler DataHandler => _dataHandler;
        
        public void Initialize()
        {
            _dataHandler = new DataHandler(_registerSaveDataEvent, _getDataEventSO);
        }

    }

}


