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
        if (_quests.Count > 0 || index < _quests.Count - 1)
        {
            _activeQuest = _quests[index];
            _activeQuest.Init();

            StoreData(_activeQuest, QuestEventType.Started);
            QuestPart firstPart = _activeQuest.CurrentQuestPart;
            if (firstPart != null)
            {
                StoreData(firstPart, QuestEventType.PartStarted);
            }

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

    private void StoreData(QuestPart questpart, QuestEventType type)
    {
        if (_storeDataEvent == null) return;

        if (_activeQuest == null) return;

        _storeDataEvent.Raise(new InteractionEvent
        {
            eventType = EventType.Quest,
            quest = _activeQuest,
            questPart = questpart,
            questEventType = type
        });
    }


    private void CompletePartQuest(QuestID questId)
    {
        if (_activeQuest == null) return; 
        
        QuestPart completedPart = _activeQuest.CurrentQuestPart;

        if (completedPart == null) return;

        
        bool completed = _activeQuest.Completed(questId); // If we finished current quest part

        if (!completed) return;

        StoreData(completedPart, QuestEventType.PartCompleted);
        
        
        if (_activeQuest.IsComplete)
        {
            if (_activeQuest.SetNewQuestOnComplete)
            {
                FinishQuest();
            }
            _updateUIEvent.Raise();
            return;
        }

        // Begin track next quest part
        QuestPart nextPart = _activeQuest.CurrentQuestPart;

        if (nextPart != null)
        {
            StoreData(nextPart, QuestEventType.PartStarted);
        }

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
