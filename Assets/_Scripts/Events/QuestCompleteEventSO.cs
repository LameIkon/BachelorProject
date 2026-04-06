using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Complete Event SO", menuName = "ScriptableObject/Events/QuestCompleteEvent")]
public class QuestCompleteEventSO : ScriptableObject
{
	public event Action<string> OnRaise;

	public void Raise(string questTitle) => OnRaise?.Invoke(questTitle);
}