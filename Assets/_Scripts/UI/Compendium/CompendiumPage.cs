using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Page for the compendium. CompendiumContent to hold the data such as text, while CompendiumUIReferences holds the ui references
/// </summary>
public class CompendiumPage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CompendiumContentSO _compendiumData;
    private CompendiumUIReferences _references;

    private void Awake()
    {
        FindUIReferences();
        SetContent(Language.English); // Initially set to English.
    }

    private void OnEnable()
    {
        LanguageUtility.OnLanguageChanged += SetLanguage;
        SetLanguage(LanguageUtility.CurrentLanguage);
    }
    private void OnDisable()
    {
        LanguageUtility.OnLanguageChanged -= SetLanguage;
    }

    /// <summary>
    /// This is technically not needed to set language and is just a safety mechanic to ensure data will be shown in
    /// cases that LanguageUtility script is not existing.
    /// </summary>
    /// <param name="language"></param>
    private void SetContent(Language language)
    {
        _references.image.sprite = _compendiumData.image;
        SetLanguage(language);
    }

    /// <summary>
    /// Get the content based on language. 
    /// </summary>
    /// <param name="language"></param>
    private void SetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                _references.title.text = _compendiumData.content._englishSerialization.title;
                _references.description.text = _compendiumData.content._englishSerialization.description;
                break;
            case Language.Danish:
                _references.title.text = _compendiumData.content._danishSerialization.title;
                _references.description.text = _compendiumData.content._danishSerialization.description;
                break;
        }
    }

    #region Initialize

    /// <summary>
    /// Hardcoded to find specific location in hierarchy. A Page consist of two children. 
    /// First child is the imageContainer and second is the text container.
    /// In These containers we need to find the image and the text fields to populate the CompendiumUIReferences
    /// </summary>
    private void FindUIReferences()
    {
        _references = new CompendiumUIReferences();
        _references.image = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        _references.title = transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _references.description = transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    #endregion
}
