using UnityEngine;
using System.Collections.Generic;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestGiveEventSO _questEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;
    [SerializeField] private Quest _activeQuest;

    public Quest ActiveQuest => _activeQuest;

	#region Unity Methods

	void OnEnable()
    {
        _questEvent.OnRaise += AddQuest;
        _questCompleteEvent.OnRaise += CompleteQuest;
    }

	private void OnDisable()
	{
        _questEvent.OnRaise -= AddQuest;
        _questCompleteEvent.OnRaise -= CompleteQuest;
	}

	#endregion

	private void AddQuest(Quest quest) 
    {
        _activeQuest = quest;
        _activeQuest.Init();
    }

    private void CompleteQuest() 
    {
        _activeQuest.Completed();
    }
}
