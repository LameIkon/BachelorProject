using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Page for the compendium. CompendiumContent to hold the data such as text, while CompendiumUIReferences holds the ui references
/// </summary>
public class CompendiumPage : MonoBehaviour
{
    [SerializeField] private CompendiumContentSO _compendiumData;
    [SerializeField] private CompendiumUIReferences _references;

    private void Awake()
    {
        FindUIReferences();
    }

    private void Start()
    {
        if (GameManager.s_language == Language.Danish)
        {
            _references.image.sprite = _compendiumData.image;
            _references.title.text = _compendiumData.content._danishSerialization.title;
            _references.description.text = _compendiumData.content._danishSerialization.description;
        }
        else
        {
            _references.image.sprite = _compendiumData.image;
            _references.title.text = _compendiumData.content._englishSerialization.title;
            _references.description.text = _compendiumData.content._englishSerialization.description;
        }


    }

    /// <summary>
    /// Hardcoded to find specific location in hierarchy. A Page consist of two children. 
    /// First child is the imageContainer and second is the text container.
    /// In These containers we need to find the image and the text fields to populate the CompendiumUIReferences
    /// </summary>
    private void FindUIReferences()
    {
        _references.image = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        _references.title = transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _references.description = transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
