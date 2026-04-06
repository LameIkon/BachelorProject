using TMPro;
using UnityEngine;
using static Quest;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Quest _quest;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private QuestCompleteEventSO _questCompleteEventSO;

    private TextMeshProUGUI _gui;

    #region Unity Methods
    void Start()
    {
        _questManager = QuestManager.Instance;
        _quest = _questManager.ActiveQuest;
        _gui = GetComponentInChildren<TextMeshProUGUI>();
        SetUpQuest();
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
    private void QuestComplete()
    {
        SetUpQuest();
        Debug.Log("Quest complete");
    }

    private void SetUpQuest() 
    {
        _gui.text = string.Empty;
        foreach (Part p in _quest.Parts)
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
