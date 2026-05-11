using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestGiveEventSO _questEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;
    [SerializeField] private LevelQuestGiveEventSO _questListEvent;
    [SerializeField] private QuestGiveProviderSO _questGiveProvider;

    [SerializeField] private StoreDataEventSO _storeDataEvent;  
    [SerializeField] private ActionEventSO _updateUIEvent;

    [Header("Quests")]
    [SerializeField] private Quest _activeQuest;
    [SerializeField] private List<Quest> _quests;
    private int _questIndex;

    #region Unity Methods


	void OnEnable()
    {
        _questEvent.OnRaise += ForceSetQuest;
        _questListEvent.OnRaise += CreateQuestList;
        _questCompleteEvent.OnRaise += CompletePartQuest;
        _questGiveProvider.Register(GetQuest);

        _activeQuest?.Init();
    }

	private void OnDisable()
	{
        _questEvent.OnRaise -= ForceSetQuest;
        _questCompleteEvent.OnRaise -= CompletePartQuest;
        _questListEvent.OnRaise -= CreateQuestList;
        _questGiveProvider.Unregister(GetQuest);
	}

    #endregion
    private void CreateQuestList(List<Quest> quests)
    {
        //Debug.Log(quests);
        //_quests = new();
        //_quests.AddRange(quests);
        SetQuest(_questIndex);
    }

    private void SetQuest(int index)
    {
        if (_quests.Count > 0)
        {
            _activeQuest = _quests[index];
            _activeQuest.Init();
            StoreData(_activeQuest, QuestEventType.Started);
            _updateUIEvent.Raise();
        }
    }

    private void FinishQuest()
    {
        if (_activeQuest != null)
        {
            StoreData(_activeQuest, QuestEventType.Completed);
            _questIndex++;
        }
        SetQuest(_questIndex);
    }

    private void ForceSetQuest(Quest quest)
    {
        FinishQuest();
        //SetQuest(int index);
        //Debug.Log($"Quest added: {quest}");
        //_activeQuest = quest;
        //_activeQuest.Init();

        //_updateUIEvent.Raise();
    }

    private void StoreData(Quest quest, QuestEventType type)
    {
        if (_storeDataEvent == null) return;

        _storeDataEvent.Raise(new InteractionEvent
        {
            eventType = EventType.Quest,
            quest = quest,
            questEventType = type
        });
    }


    private void CompletePartQuest(QuestID questId)
    {
        if (_activeQuest == null) return; 
        if (_activeQuest.Completed(questId)) // If we finished current quest part
        {
            StoreData(_activeQuest, QuestEventType.PartCompleted);

            if (_activeQuest.IsComplete && _activeQuest.SetNewQuestOnComplete)
            {
                FinishQuest();
                return;
            }
        }
        _updateUIEvent.Raise();
        //_updateUIEvent.Raise();
        //_isQuestComplete = _activeQuest.IsComplete;

        //StoreData(_activeQuest, QuestEventType.PartCompleted);

        //if(_isQuestComplete) _toggleEvent.Raise(new UIRequest(UIType.NextLevelPopUp, UIInteractionSource.UIInternal));
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
