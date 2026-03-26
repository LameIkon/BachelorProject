using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : ISceneLoader
{
    private AsyncSceneLoader() { }

    private static AsyncSceneLoader _instance;

    public static AsyncSceneLoader Instance 
    { 
        get
        {
            if (_instance == null) 
            {
                _instance = new AsyncSceneLoader();
            }
            return _instance;
        }
    } 


    // The Coroutine for loading the scenes 
    public IEnumerator LoadScenes(SceneField[] scenes)
    {
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

    }

    public IEnumerator LoadScenes(List<SceneField> scenes) 
    {
        return LoadScenes(scenes.ToArray());
    }

    public IEnumerator UnloadScenes(SceneField[] scenes)
    {

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

    public IEnumerator UnloadScenes(List<SceneField> scenes)
    {
        return UnloadScenes(scenes.ToArray());
    }

}

public interface ISceneLoader 
{
    IEnumerator LoadScenes(SceneField[] scenes);
    IEnumerator UnloadScenes(SceneField[] scenes);
    
}