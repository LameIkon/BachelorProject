using TMPro;
using UnityEngine;

public class LocalizedTMP : MonoBehaviour, ILanguage
{
    private TextMeshProUGUI _text;
    [SerializeField] private LocalizedContentSO _localizedContent;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
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

    public void SetLanguage(Language language)
    {
        LocalizedText data = _localizedContent?.Get(language); // Get context in the selected language

        if (data == null) return;
        {
            _text.text = data.title;
        }
    }
}