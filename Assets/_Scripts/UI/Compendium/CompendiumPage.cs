using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Page for the compendium. CompendiumContent to hold the data such as text, while CompendiumUIReferences holds the ui references
/// </summary>
public class CompendiumPage : MonoBehaviour, ILanguage
{
    [Header("Data")]
    [SerializeField] private CompendiumContentSO _compendiumData;

    [Header("Button UI")]
    private TextMeshProUGUI _buttonTitle;
    private GameObject _buttonObject; // to be changed
    
    [Header("Page UI")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;


    public string title {get; private set; } = "Unasigned";
    public Button button {get; private set; } // Access from outside to call button 
    public CompendiumID id {get; private set; }

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
    public void SetLanguage(Language language)
    {
        LocalizedContent data = _compendiumData.content.Get(language); // Get context in the selected language

        _title.text = data.title;
        _description.text = data.description;
        title = data.title;

        UpdateButtonText(title);
    }

    private void UpdateButtonText(string title)
    {
        if (_buttonTitle == null) return;

        _buttonTitle.text = title;
    }

    /// <summary>
    /// Enable or disable button
    /// </summary>
    /// <param name="state"></param>
    public void ToggleButton(bool state)
    {
        _buttonObject.gameObject.SetActive(state);
    }

    #region Initialize

    /// <summary>
    /// Hardcoded to find specific location in hierarchy. A Page consist of two children. 
    /// First child is the imageContainer and second is the text container.
    /// In These containers we need to find the image and the text fields to populate the CompendiumUIReferences
    /// </summary>
    public void Initialize(GameObject button)
    {
        Debug.Log("page initialized");
        id = _compendiumData.compendiumID;
        _buttonObject = button;
        this.button =  _buttonObject.GetComponentInChildren<Button>();
        _buttonTitle = _buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        _image.sprite = _compendiumData.image;

        SetLanguage(LanguageUtility.CurrentLanguage);

        UpdateButtonText(title);
    }
    #endregion
}
