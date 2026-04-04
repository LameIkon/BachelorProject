using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used on buttons to call whenever we close the game.
/// </summary>

public class ExitGame : MonoBehaviour
{
    [SerializeField] private SaveAllDataEventSO _saveAllData;

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(ExitGameButton);
        }
    }


    public void ExitGameButton()
    {
        _saveAllData?.Raise();

        // only quits the editor if its the unity editor application, otherwise it ignores this code
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        //when the game is not in the unity editor application quit with this method
        Application.Quit();
    }
}
