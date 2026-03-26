using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader
{
    private readonly LevelData[] _levels;

    private readonly Dictionary<LevelName, LevelData> _levelsDict;
    private readonly LevelName _previousLevelLoaded;

    public AsyncSceneLoader(LevelName firstSceneLoad, LevelData[] levels) 
    { 
        _previousLevelLoaded = firstSceneLoad;
        _levels = levels;

        _levelsDict = new Dictionary<LevelName, LevelData>();
        InitLevels();
    }

    private void InitLevels() 
    {
        foreach (LevelData data in _levels) 
        {
            _levelsDict.Add(data.Name, data);
        }
    }


    // The Coroutine for loading the scenes 
    public IEnumerator LoadScenes(LevelName level)
    {
        SceneField[] scenes = _levelsDict[level].Scenes;

        foreach (SceneField scene in scenes) // looping over all the scenes in the array
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded) // We check that the scene is not loaded such that it does not load already loaded scenes
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); // This is how we load multiple scenes together
            }
            while (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                yield return null; // This will yield untill the scene has been loaded, if this was not here the game lags when to many scenes are loaded at once
            }
        }


        InputReader.SetState(_levelsDict[level].GameState);
    }

    public IEnumerator UnloadScenes()
    {
        SceneField[] scenes = _levelsDict[_previousLevelLoaded].Scenes;

        foreach (SceneField scene in scenes)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.None);
            }
            while (SceneManager.GetSceneByName(scene).isLoaded)
            {
                yield return null;
            }

        }
    }
}

public interface ISceneLoader 
{
    IEnumerator LoadScenes(SceneField[] scenes);
    IEnumerator UnloadScenes(SceneField[] scenes);
    
}