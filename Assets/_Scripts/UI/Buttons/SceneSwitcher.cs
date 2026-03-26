using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private SceneLoadEventSO _onSceneLoad;
    [SerializeField] private LevelName _levelName;

    public void SwitchScene()
    {
        _onSceneLoad.Raise(_levelName);
    }
}
