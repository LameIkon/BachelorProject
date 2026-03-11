using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Page for the compendium. CompendiumContent to hold the data such as text, while CompendiumUIReferences holds the ui references
/// </summary>
public class CompendiumPage : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CompendiumContentSO _compendiumData;

    [Header("Button UI")]
    private TextMeshProUGUI _buttonTitle;
    private GameObject _buttonObject; // to be changed
    
    [Header("Page UI")]
    //[SerializeField] private Image _image;
    //[SerializeField] private TextMeshProUGUI _title;
    //[SerializeField] private TextMeshProUGUI _description;
    CompendiumUIReferences _references;

    //public event Action<string> OnTitleChanged;

    public string title {get; private set; } = "Unasigned";

    private void Awake()
    {
        FindUIReferences();
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
        title = data.title;

        UpdateButtonText(title);
    }

    private void UpdateButtonText(string title)
    {
        if (_buttonTitle == null) return;

        _buttonTitle.text = (title);
    }

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
        _buttonObject = button;
        _buttonTitle = _buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        _references.image.sprite = _compendiumData.image;

        UpdateButtonText(title);
    }

    private void FindUIReferences()
    {
        _references = new CompendiumUIReferences();
        _references.image = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        _references.title = transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _references.description = transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    #endregion
}
