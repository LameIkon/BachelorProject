using TMPro;
using UnityEngine;

public class CompendiumManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _buttonPrefab;

    [Header("Data")]
    [SerializeField] private Transform _buttonList;
    [SerializeField] private CompendiumPage[] entries;
    [SerializeField] private UIModule _UIModule;

    private void Start()
    {
        Initialize();
        _inputField.onValueChanged.AddListener(Search); // Listen for every time a change has occured on the input field
    }

    private void Initialize()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            PageButton pageButton = _buttonPrefab.GetComponent<PageButton>();
            pageButton.pageIndex = i; // Set index of button to corresponding page index

            GameObject button = Instantiate(_buttonPrefab, _buttonList);
            entries[i].Initialize(button);
        }

        _UIModule.pageModule.SetupButtons(); // Register buttons
    }

    /// <summary>
    /// Search the list of buttons by looking at the text in the text field. If any combination of letters matches any of the words on the buttons
    /// It will display and disable those not with a match.
    /// </summary>
    /// <param name="input"></param>
    private void Search(string input)
    {
        input = input.ToLower();

        foreach (CompendiumPage entry in entries)
        {
            string title = entry.title.ToLower();

            bool match = title.Contains(input); // Check if there is a match

            entry.ToggleButton(match);
        }
    }
}
