using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropDownSelector : MonoBehaviour
{
    private Dropdown _dropdownLanguageOptions;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _dropdownLanguageOptions = GetComponent<Dropdown>();




        
        //_dropdownLanguageOptions.AddOptions();
    }

    private IEnumerable<Dropdown.OptionData> CreateOptionData()
    {

        return null;
    }
}
