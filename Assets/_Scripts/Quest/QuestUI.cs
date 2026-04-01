using TMPro;
using UnityEngine;
using static Quest;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Quest _quest;
    [SerializeField] private QuestGiveEventSO _questEventSO;

    private TextMeshProUGUI _gui;

    #region Unity Methods
    void Start()
    {
        _gui = GetComponentInChildren<TextMeshProUGUI>();
        _quest.Init();
        SetUpQuest(_quest);
    }

    private void OnEnable()
    {
        InputReader.s_OnInteractEvent += QuestComplete;
    }

    private void OnDisable()
    {
        InputReader.s_OnInteractEvent -= QuestComplete;
    }
    #endregion

    #region Handlers
    private void HandleQuestEvent(Quest newQuest) 
    {
        _quest = newQuest;
        _quest.Init();
        SetUpQuest(_quest);
    }


    #endregion

    #region Own Methods
    private void QuestComplete(Vector2 vector)
    {
        _quest.Completed();
        SetUpQuest(_quest);
        Debug.Log("Quest added");
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
