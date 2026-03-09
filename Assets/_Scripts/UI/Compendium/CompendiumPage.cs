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

        _references.image.sprite = _compendiumData.image;
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
    /// Get the content based on language. 
    /// </summary>
    /// <param name="language"></param>
    private void SetLanguage(Language language)
    {
        LocalizedContent data = _compendiumData.content.Get(language); // Get context in the selected language

        _references.title.text = data.title;
        _references.description.text = data.description;
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
