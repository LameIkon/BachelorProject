using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompendiumManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private CompendiumPage[] entries;

    [SerializeField] private GameObject _buttonPrefab;

    [SerializeField] private Transform _buttonList;

    [SerializeField] private UIModule _UIModule;

    private void Awake()
    {
        //Initialize();
    }

    private void Start()
    {
        Initialize();
        _inputField.onValueChanged.AddListener(Search);
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

        _UIModule.pageModule.SetupButtons();
    }

    private void Search(string input)
    {
        input = input.ToLower();

        foreach (var entry in entries)
        {
            string title = entry.title.ToLower();

            bool match = title.Contains(input);

            entry.ToggleButton(match);
        }
    }
}
