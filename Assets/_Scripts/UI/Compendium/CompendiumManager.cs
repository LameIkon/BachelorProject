using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompendiumManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _buttonList;

    [Header("Data")]
    [SerializeField] private UIModule _UIModule;
    [SerializeField] private CompendiumPage[] _entries;

    // Navigation tracking
    private Stack<PageButton> _backStack = new Stack<PageButton>();
    private Stack<PageButton> _forwardStack = new Stack<PageButton>();
    private bool _isNavigatingFromHistory = false;
    private PageButton _currentPage;


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


            // Create Navigation tracking
            if (button.TryGetComponent(out Button compendiumButton))
            {
                Debug.Log("compendiumButton found");
                compendiumButton.onClick.AddListener(() =>
                {
                    NavigateTo(pageButton);
                });
            }
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

    #region Navigation Remembering Logic
    private void NavigateTo(PageButton pageButton)
    {
        if (!_isNavigatingFromHistory)
        {
            if (_currentPage != null)
            {
                _backStack.Push(_currentPage); // Only push if this is a "real click"
                _forwardStack.Clear(); // Clear forward stack on new navigation
            }
        }

        _currentPage = pageButton;

        Debug.Log($"Navigated to page: {_currentPage.pageIndex}");
    }

    private void NavigateToFromHistory(PageButton pageButton)
    {
        _isNavigatingFromHistory = true;   // Temporarily mark that we are navigating from history
        pageButton.GetComponent<Button>().onClick.Invoke();
        _isNavigatingFromHistory = false;  // Reset the flag
    }

    public void GoBack() // Called from button
    {
        Debug.Log("Try Go back");
        if (_backStack.Count == 0) return;

        _forwardStack.Push(_currentPage); // Save current to forward stack

        PageButton previous = _backStack.Pop();
        NavigateToFromHistory(previous);

    }

    public void GoForward() // Called from button
    {
        Debug.Log("Try Go Forward");
        if (_forwardStack.Count == 0) return;

        _backStack.Push(_currentPage); // Save current to back stack
        PageButton next = _forwardStack.Pop();
        NavigateToFromHistory(next);
    }
    #endregion

}
