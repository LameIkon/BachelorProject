using System.Collections.Generic;
using System.Linq;
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

    [Header("Navigation Tracking")]
    [SerializeField] private TextMeshProUGUI _historyNavigationContainer;
    private Stack<PageButton> _backStack = new Stack<PageButton>();
    private Stack<PageButton> _forwardStack = new Stack<PageButton>();
    private bool _isNavigatingFromHistory = false;
    private PageButton _currentPage;
    private int _historyLimit = 20;


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
                //Debug.Log("compendiumButton found");
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
        if (_currentPage == pageButton) return;

        if (!_isNavigatingFromHistory && _currentPage != null) // Only push if we made the call from the forward or back button
        {
            PushToStack(_backStack, _currentPage);
            _forwardStack.Clear(); // Clear forward stack on new navigation
            
        }

        _currentPage = pageButton;

        PrintNavigationHistory();
    }

    /// <summary>
    /// Display navigation history 
    /// </summary>
    private void PrintNavigationHistory()
    {
        // Join each element in the stack by first reversing it to get the oldest value, then get its text for a name lastly join together with →  
        //string backList = string.Join(" → ", _backStack.Reverse().Select(page => page.GetComponentInChildren<TextMeshProUGUI>().text));
        //string forwardList = string.Join(" → ", _forwardStack.Reverse().Select(page => page.GetComponentInChildren<TextMeshProUGUI>().text));
        
        // Show what will come next or what the previous is
        string previous = _backStack.Count > 0 ? _backStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text : "None";
        string forward = _forwardStack.Count > 0 ? _forwardStack.Peek().GetComponentInChildren<TextMeshProUGUI>().text : "None";

        string current = _currentPage != null ? _currentPage.GetComponentInChildren<TextMeshProUGUI>().text : "None";
        string displayNavigationHistory = $"Back: {previous} | Current: {current} | Forward: {forward}";

        _historyNavigationContainer.text = displayNavigationHistory;
        //Debug.Log(displayNavigationHistory);
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


    private void PushToStack(Stack<PageButton> stack, PageButton page)
    {
        stack.Push(page);

        // Reduce stack if it exceeds limit
        while (stack.Count > _historyLimit)
        {
            PageButton[] arr = stack.ToArray();
            stack.Clear();
            for (int i = 0; i < arr.Length - 1; i++) // remove last entry
            {
                stack.Push(arr[i]);
            }
        }
    }
    #endregion

}
