using System;
using UnityEngine;

/// <summary>
/// Depricated script. Will probably be deleted at some point if we find no use for it.
/// </summary>
[CreateAssetMenu(fileName = "Quest Start Event SO", menuName = "ScriptableObject/Events/QuestGive")]
public class QuestGiveEventSO : ScriptableObject
{
	public event Action<Quest> OnRaise;

	public void Raise(Quest quest) => OnRaise?.Invoke(quest);
}
