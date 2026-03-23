using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AsyncSceneLoader : MonoBehaviour
{

    [SerializeField] private SceneField[] _loadScenes; // This is the array of scenes that get loaded
    [SerializeField] private SceneField[] _unloadScenes; // This is the array of scenes that get unloaded

    private void Start()
    {
        StartCoroutine(LoadScene(_loadScenes));
    }

    // The Coroutine for loading the scenes 
    private IEnumerator LoadScene(SceneField[] scenes)
    {
        foreach (var scene in scenes) // looping over all the scenes in the array
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

    private IEnumerator UnloadScene(SceneField[] scenes)
    {

        foreach (var scene in scenes)
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