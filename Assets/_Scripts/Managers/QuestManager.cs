using UnityEngine;
using System.Collections.Generic;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private QuestGiveEventSO _questEvent;
    [SerializeField] private List<Quest> _activeQuests;
    private List<Quest> _completedQuests;

    public List<Quest> ActiveQuests => _activeQuests;
    public List<Quest> CompletedQuests => _completedQuests;

	#region Unity Methods

	void Start()
    {
        _activeQuests = new List<Quest>();
        _completedQuests = new List<Quest>();
        _questEvent.OnRaise += AddQuest;
    }

	private void OnDisable()
	{
        _questEvent.OnRaise -= AddQuest;
	}

	#endregion

	private void AddQuest(Quest quest) 
    {
        _activeQuests.Add(quest);
    }

    private void CompleteQuest(Quest quest) 
    {
        if (_activeQuests.Contains(quest)) 
        {
            _activeQuests.Remove(quest);
            _completedQuests.Add(quest);
        }
    }
}
