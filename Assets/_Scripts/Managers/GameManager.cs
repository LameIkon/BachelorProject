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
        _sceneLoader = new AsyncSceneLoader(_firstSceneToLoad.Id, _levels);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadLevel(_firstSceneToLoad.Id);
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


    private void LoadLevel(int levelId) 
    {
        StartCoroutine(_sceneLoader.UnloadScenes());

        StartCoroutine(_sceneLoader.LoadScenes(levelId));
        
        // TODO: Send Quest data through the QuestGiveEventSO, would like to refactor this so it
        // uses the LevelData as a parameter instead of the levelId.
    }

}
