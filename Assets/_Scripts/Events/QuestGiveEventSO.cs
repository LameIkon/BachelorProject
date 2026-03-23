using System;
using UnityEngine;

/// <summary>
/// This sends a Quest event.
/// </summary>
[CreateAssetMenu(fileName = "Quest Start Event SO", menuName = "ScriptableObject/Events/QuestGive")]
public class QuestGiveEventSO : ScriptableObject
{
	public event Action<Quest> OnRaise;

	public void Raise(Quest quest) => OnRaise?.Invoke(quest);
}
