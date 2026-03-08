using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is work in progress
/// </summary>
public class PageUIOverrideModule
{
    private readonly GameObject _pageContainer;
    private readonly Image _image;
    private readonly TextMeshProUGUI _textBody;
    private readonly TextMeshProUGUI _title;

    private readonly Dictionary<int, GameObject> _pages;
    private readonly PageHelper _pageHelper;

    public PageUIOverrideModule(GameObject pageContainer, Image image, TextMeshProUGUI textBody, TextMeshProUGUI title)
    {
        _pageContainer = pageContainer;
        _image = image;
        _textBody = textBody;
        _title = title;

        _pageHelper = new PageHelper();
        _pages = _pageHelper.InitializePages(_pageContainer);
        SetupButtons();
    }

    public void SwitchPage(int pageKey)
    {
        //_pageHelper.OverridePage(_pages, pageKey);
        _pageHelper.SwitchToPage(_pages, pageKey);
    }

    private void SetupButtons() // Find all buttons 
    {
        foreach (Button button in _pageContainer.GetComponentsInChildren<Button>(true))
        {
            if (button.TryGetComponent(out PageButton pageButton)) // If button have the PageButton script
            {
                int pageKey = pageButton.pageIndex;
                button.onClick.AddListener(() => SwitchPage(pageKey));
            }
        }
    }
}