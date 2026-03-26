using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CrosshairHandler _crosshairHandler;
    private AsyncSceneLoader _sceneLoader;

    [SerializeField] private LevelData[] _levels;
    [SerializeField] private SceneLoadEventSO _sceneLoadEventSO;

    private Dictionary<LevelName, SceneField[]> _levelsDict;
    private LevelName priviousLevelLoaded;

    #region Unity Method 
    protected override void Awake()
    {
        base.Awake();
        _crosshairHandler = new CrosshairHandler();
        _sceneLoader = AsyncSceneLoader.Instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitLevels();
        priviousLevelLoaded = LevelName.MainMenu;
        LoadScenes(LevelName.MainMenu);

    }

    private void OnEnable()
    {
        _sceneLoadEventSO.OnRaise += LoadScenes;
    }

    private void OnDisable()
    {
        _sceneLoadEventSO.OnRaise -= LoadScenes;
    }

    #endregion

    private void OnApplicationQuit()
    {
        _crosshairHandler.Dispose();
        _levelsDict.Clear(); 
    }

    private void InitLevels() 
    {
        _levelsDict = new Dictionary<LevelName, SceneField[]>();

        foreach (LevelData levelData in _levels) 
        {
            _levelsDict.Add(levelData.Name, levelData.Scenes);
        }
    }


    private void LoadScenes(LevelName levelName) 
    {
        if (!_levelsDict.ContainsKey(levelName)) return;

        StartCoroutine(_sceneLoader.UnloadScenes(_levelsDict[priviousLevelLoaded]));

        StartCoroutine(_sceneLoader.LoadScenes(_levelsDict[levelName]));
        priviousLevelLoaded = levelName;
    }

}

public enum LevelName 
{
    MainMenu,
    Tutorial,
    Level1,
    Level2,
    Level3
}
