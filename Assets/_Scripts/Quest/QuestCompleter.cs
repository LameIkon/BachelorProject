using UnityEngine;

public class QuestCompleter : MonoBehaviour, IQuestCompleter
{

	[SerializeField] private QuestCompleteEventSO _questComplete;


	public QuestCompleteEventSO QuestComplete()
	{
		throw new System.NotImplementedException();
	}
}


