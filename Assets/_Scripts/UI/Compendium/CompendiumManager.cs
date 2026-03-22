using TMPro;
using UnityEngine;

public class CompendiumManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _buttonList;

    [Header("Data")]
    [SerializeField] private UIModule _UIModule;
    [SerializeField] private CompendiumPage[] _entries;

    private void Start()
    {
        Initialize();
        _inputField.onValueChanged.AddListener(Search); // Listen for every time a change has occured on the input field
    }

    private void Initialize()
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            GameObject button = Instantiate(_buttonPrefab, _buttonList);
            PageButton pageButton = button.GetComponent<PageButton>();
            pageButton.pageIndex = i; // Set index of button to corresponding page index
            _entries[i].Initialize(button);
        }
    }

    /// <summary>
    /// Search the list of buttons by looking at the text in the text field. If any combination of letters matches any of the words on the buttons
    /// It will display and disable those not with a match.
    /// </summary>
    /// <param name="input"></param>
    private void Search(string input)
    {
        input = input.ToLower();

        foreach (CompendiumPage entry in _entries)
        {
            string title = entry.title.ToLower();

            bool match = title.Contains(input); // Check if there is a match

            entry.ToggleButton(match);
        }
    }
}
