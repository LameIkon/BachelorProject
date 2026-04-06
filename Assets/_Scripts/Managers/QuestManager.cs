using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestGiveEventSO _questEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;
    [SerializeField] private QuestGiveProviderSO _questGiveProvider;
    [SerializeField] private Quest _activeQuest;

    [SerializeField] private ActionEventSO _updateUIEvent;

    public Quest ActiveQuest => _activeQuest;

	#region Unity Methods

	void OnEnable()
    {
        _questEvent.OnRaise += AddQuest;
        _questCompleteEvent.OnRaise += CompleteQuest;
        _questGiveProvider.Register(GetQuest);

        _activeQuest?.Init();
    }

	private void OnDisable()
	{
        _questEvent.OnRaise -= AddQuest;
        _questCompleteEvent.OnRaise -= CompleteQuest;
        _questGiveProvider.Unregister(GetQuest);

	}

	#endregion

	private void AddQuest(Quest quest) 
    {
        Debug.Log($"Quest added: {quest}");
        _activeQuest = quest;
        _activeQuest.Init();
        _updateUIEvent.Raise();

    }

    private void CompleteQuest(QuestID questId) 
    {
        _activeQuest.Completed(questId);
        _updateUIEvent.Raise();

    }

    private Quest GetQuest() 
    {
        if (_activeQuest != null) 
        {
            return _activeQuest;
        }
        return null;
    }
}
