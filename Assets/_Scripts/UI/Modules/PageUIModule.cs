using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is for ui systems to know what canvas to work with and which pages can be found assigned to a dictionary
/// to compare the page with a key for buttons to find that page
/// </summary>

public class PageUIModule
{
    private readonly PageSettings _pageSettings;
    private readonly Dictionary<int, GameObject> _pages; // index of a page (like a page in a book)
    private readonly PageHelper _pageHelper;

    public PageUIModule(PageSettings settings)
    {
        _pageSettings = settings;
        _pageHelper = new PageHelper();
        _pages = _pageHelper.InitializePages(_pageSettings.pageContainer);
        SetupButtons();
    }

    public void SwitchPage(int pageKey)
    {
        _pageHelper.SwitchToPage(_pages, pageKey);
    }

    public void SetupButtons() // Find all buttons 
    {
        foreach (Button button in _pageSettings.pageContainer.GetComponentsInChildren<Button>(true))
        {
            if (button.TryGetComponent(out PageButton pageButton)) // If button have the PageButton script
            {
                int pageKey = pageButton.pageIndex;
                button.onClick.AddListener(() => { 
                    SwitchPage(pageKey);
                    Debug.Log(pageKey);
                    });
            }
        }

        if (!_pageSettings.buttonContainer) return; // Return if we did not assign optional buttoncontainer

        foreach (Button button in _pageSettings.buttonContainer.GetComponentsInChildren<Button>(true))
        {
            if (button.TryGetComponent(out PageButton pageButton)) // If button have the PageButton script
            {
                int pageKey = pageButton.pageIndex;
                button.onClick.AddListener(() => { 
                    SwitchPage(pageKey);
                    Debug.Log(pageKey);
                    }); 
            }
        }
    }
}
