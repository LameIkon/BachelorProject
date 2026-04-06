using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private SceneLoadEventSO _onSceneLoad;
    [SerializeField] private LevelData _levelToLoad;

    public void SwitchScene()
    {
        _onSceneLoad.Raise(_levelToLoad);
    }
}
