using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for ui systems to know about what canvas we are working with, getting the container for 
/// which the multiple pages can be found and a dictionary to compare the page with a key for buttons to find
/// that page
/// </summary>

public class PageUIModule
{
    private readonly GameObject _pageContainer;
    private readonly Dictionary<int, GameObject> _pages;
    private readonly PageHelper _pageHelper;

    public PageUIModule(GameObject pageContainer)
    {
        _pageContainer = pageContainer;
        _pageHelper = new PageHelper();
        _pages = _pageHelper.InitializePages(_pageContainer);
    }

    public void SwitchPage(int pageKey)
    {
        _pageHelper.SwitchToPage(_pages, pageKey);
    }
}
