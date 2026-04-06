using UnityEngine;

[CreateAssetMenu(fileName = "Quest Completer", menuName = "ScriptableObject/Quest/Completer")]
public class QuestCompleterSO : ScriptableObject
{

	[SerializeField] private QuestCompleteEventSO _questComplete;
	[SerializeField] private string _questTitle;

	public void CompleteQuest() 
	{
		_questComplete.Raise(_questTitle);
	}
}


