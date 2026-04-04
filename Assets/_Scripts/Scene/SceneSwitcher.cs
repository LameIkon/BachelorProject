using UnityEngine;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private SceneLoadEventSO _onSceneLoad;
    [SerializeField] private LevelData _levelToLoad;

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(SwitchScene);
        }
    }

    public void SwitchScene()
    {
        _onSceneLoad.Raise(_levelToLoad.Id);
    }
}
