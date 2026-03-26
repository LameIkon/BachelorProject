using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CrosshairHandler _crosshairHandler;
    private AsyncSceneLoader _sceneLoader;

    [SerializeField] private LevelData[] _levels;
    [SerializeField] private SceneLoadEventSO _sceneLoadEventSO;

    [SerializeField] private LevelName _firstSceneToLoad;

    #region Unity Method 
    protected override void Awake()
    {
        base.Awake();
        _crosshairHandler = new CrosshairHandler();
        _sceneLoader = new AsyncSceneLoader(_firstSceneToLoad, _levels);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadScenes(_firstSceneToLoad);
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
    }


    private void LoadScenes(LevelName levelName) 
    {
        StartCoroutine(_sceneLoader.UnloadScenes());

        StartCoroutine(_sceneLoader.LoadScenes(levelName));
    }

}
