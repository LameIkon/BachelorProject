using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Does not work currently.
/// This class holds the datas for the levels and can call the next one in line.
/// </summary>
[CreateAssetMenu(fileName = "Quests Data", menuName = "ScriptableObject/Quest/Data")]
public class LevelsData : ScriptableObject 
{
	[SerializeField] private LevelData[] _levels;

	private Stack<LevelData> levels;

	public void Init() 
	{
		levels = new Stack<LevelData>();
		foreach (LevelData ld in _levels) 
		{
			levels.Push(ld);
		}
	}

	public LevelData GetNextLevel()
	{
		if (levels.Count > 0)
		{
			return levels.Pop();
		}
		return null;
	}

}

