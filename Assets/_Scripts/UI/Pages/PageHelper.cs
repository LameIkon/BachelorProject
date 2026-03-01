using System.Collections.Generic;
using UnityEngine;

public class PageHelper
{
    public Dictionary<int, GameObject> InitializePages(GameObject container)
    {
        Dictionary<int, GameObject> pages = new Dictionary<int, GameObject>();
        int pageCount = container.transform.childCount; // Get the amount of pages that is on the gameobject

        for (int key = 0; key < pageCount; key++)
        {
            Transform page = container.transform.GetChild(key);
            pages.Add(key, page.gameObject); // Assign a page-value to the key. Used for buttons to know what page to go to
        }
        
        SwitchToPage(pages, 0); // Initialize to show first page
        return pages;
    }
    
    public void SwitchToPage(Dictionary<int, GameObject> pages, int key)
    {
        if (!pages.ContainsKey(key)) return; // If by chance we don't have the selected key (only happens if we write a wrong value) we don't do anything

        foreach (GameObject page in pages.Values) // Set all pages to inactive
        {
            page.SetActive(false);
        }

        pages[key].SetActive(true); // Set the selected page to active
    }
    
    public void ToggleContainer(GameObject canvas, Dictionary<int, GameObject> pages = null)
    {
        bool isActive = canvas.activeInHierarchy; // Check if its currently active or not and set is as bool state
        
        if (!isActive && pages != null) // If opening canvas then show first page
        {
            SwitchToPage(pages, 0);
        }
        
        canvas.SetActive(!isActive); // Set activeState to opposite of given bool
    }
}