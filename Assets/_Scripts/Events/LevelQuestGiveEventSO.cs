using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This sends a all quests for a scene
/// </summary>
[CreateAssetMenu(fileName = "Quest list SO", menuName = "ScriptableObject/Events/Quest list")]
public class LevelQuestGiveEventSO : ScriptableObject
{
	public event Action<List<Quest>> OnRaise;

	public void Raise(List<Quest> quest) => OnRaise?.Invoke(quest);
}
