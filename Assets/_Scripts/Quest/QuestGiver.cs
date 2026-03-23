using UnityEngine;

public class QuestGiver : MonoBehaviour, IQuestGiver
{
	[SerializeField] private QuestGiveEventSO _questGiveEventSO;
	[SerializeField] private Quest _quest;

    #region Unity Method
    void Start()
    {
		GiveQuest(_quest);
    }


	#endregion
	public void GiveQuest(Quest quest)
	{
		_questGiveEventSO.Raise(quest);
	}
}
