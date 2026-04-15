using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestGiveEventSO _questEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;
    [SerializeField] private QuestGiveProviderSO _questGiveProvider;
    [SerializeField] private Quest _activeQuest;
    [SerializeField] private UIToggleEventSO _toggleEvent;

    [SerializeField] private StoreDataEventSO _storeDataEvent;
    
    [SerializeField] private ActionEventSO _updateUIEvent;

    [SerializeField] private bool _isQuestComplete;

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

        //_storeDataEvent.Raise(new InteractionEvent
        //{
        //    eventType = EventType.Quest,
        //    quest = quest,
        //    questEventType = QuestEventType.Started
        //});

        _updateUIEvent.Raise();
    }

    private void CompleteQuest(QuestID questId)
    {
        if (_activeQuest == null) return; 
        _activeQuest.Completed(questId);
        _updateUIEvent.Raise();
        _isQuestComplete = _activeQuest.IsComplete;

        if(_isQuestComplete) _toggleEvent.Raise(new UIRequest(UIType.NextLevelPopUp, UIInteractionSource.UIInternal));
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
