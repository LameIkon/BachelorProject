using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour, ILanguage
{
    [SerializeField] private QuestGiveProviderSO _questProvider;
    [SerializeField] private ActionEventSO _updatedUIEventSO;

    private TextMeshProUGUI _gui;

    #region Unity Methods
    void Start()
    {
        _gui = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void OnEnable()
    {
        LanguageUtility.OnLanguageChanged += SetLanguage;
        _updatedUIEventSO.OnRaise += UpdateUI;
    }

    private void OnDisable()
    {
        LanguageUtility.OnLanguageChanged -= SetLanguage;
        _updatedUIEventSO.OnRaise -= UpdateUI;
    }
    #endregion

    #region Own Methods
    private void UpdateUI() 
    {
        _gui.text = string.Empty;
        if (_questProvider.GetQuest().IsComplete) 
        {
            _gui.text = "Quest Complete\n " +
                "Go to next level";
            return;
        }

        foreach (QuestPart p in _questProvider.GetQuest().Parts)
        {
            if (!p.IsPartComplete)
            {
                _gui.text += p.ToString() + "\n";
            }
            else 
            {
                _gui.text += "<s>" + p.ToString() + "</s>\n" ; 
            }
        }
    }

    public void SetLanguage(Language language)
    {
        throw new System.NotImplementedException();
    }

    #endregion

}
