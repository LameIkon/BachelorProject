using TMPro;
using UnityEngine;
using static Quest;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private QuestCompleteEventSO _questCompleteEventSO;
    [SerializeField] private QuestGiveProviderSO _questProvider;

    private TextMeshProUGUI _gui;

    #region Unity Methods
    void Start()
    {
        _gui = GetComponentInChildren<TextMeshProUGUI>();
        SetUpQuest(_questProvider.GetQuest());
    }

    private void OnEnable()
    {
        _questCompleteEventSO.OnRaise += QuestComplete;
    }

    private void OnDisable()
    {
        _questCompleteEventSO.OnRaise -= QuestComplete;
    }
    #endregion

    #region Own Methods
    private void QuestComplete(string questTitle)
    {
        SetUpQuest(_questProvider.GetQuest());
        Debug.Log("Quest complete");
    }

    private void SetUpQuest(Quest quest) 
    {
        _gui.text = string.Empty;
        foreach (Part p in quest.Parts)
        {
            if (!p.IsPartComplete)
            {
                _gui.text += p.Description + "\n";
            }
            else 
            {
                _gui.text += "<s>" + p.Description + "</s>\n" ; 
            }
        }
    }

    #endregion

}
