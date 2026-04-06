using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CrosshairHandler _crosshairHandler;
    private AsyncSceneLoader _sceneLoader;

    [SerializeField] private LevelData[] _levels;
    [SerializeField] private SceneLoadEventSO _sceneLoadEventSO;
    [SerializeField] private QuestGiveEventSO _questGiveEventSO;

    [SerializeField] private LevelData _firstSceneToLoad;

    #region Unity Method 
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _crosshairHandler = new CrosshairHandler();
        _sceneLoader = new AsyncSceneLoader();
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
        Debug.Log($"Quest Give event: {levelData.LevelQuest}");
        _questGiveEventSO.Raise(levelData.LevelQuest);
    }

}
