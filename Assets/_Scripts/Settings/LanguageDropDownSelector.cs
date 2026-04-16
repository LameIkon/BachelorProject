using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageDropDownSelector : MonoBehaviour
{
    private TMP_Dropdown _dropdownLanguageOptions;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _dropdownLanguageOptions = GetComponent<TMP_Dropdown>();

        _dropdownLanguageOptions.ClearOptions();

        List<TMP_Dropdown.OptionData> optionsData = new List<TMP_Dropdown.OptionData>();

        foreach (Language language in Enum.GetValues(typeof(Language)))
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData
            {
                text = language.ToString(),
            };

            optionsData.Add(optionData);
        }

        _dropdownLanguageOptions.AddOptions(optionsData);

        _dropdownLanguageOptions.onValueChanged.AddListener(value => SetLanguage(value));

        // Update display to show the current language
        Language currentLanguage = LanguageUtility.CurrentLanguage;
        _dropdownLanguageOptions.value = (int)currentLanguage;

    }

    private void SetLanguage(int value)
    {
        Language language = (Language)value;

        LanguageUtility.SetLanguage(language);
    }
}
