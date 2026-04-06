using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader
{
    private LevelData _previousLevelLoaded;

    public AsyncSceneLoader() 
    { 
        
    }

    // The Coroutine for loading the scenes 
    public IEnumerator LoadScenes(LevelData levelData)
    {
        _previousLevelLoaded = levelData;

        foreach (SceneField scene in levelData.Scenes) // looping over all the scenes in the array
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

        InputReader.SetState(levelData.GameState);
    }

    public IEnumerator UnloadScenes()
    {
        if (_previousLevelLoaded == null) yield break; 
        SceneField[] scenes = _previousLevelLoaded.Scenes;

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