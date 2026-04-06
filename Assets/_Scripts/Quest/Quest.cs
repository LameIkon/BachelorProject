using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "ScriptableObject/Quest")]
public class Quest : ScriptableObject
{
	[SerializeField] private List<Part> _parts;
	private int _index;

	public void Init() 
	{
		_index = 0;
		foreach (Part p in _parts) 
		{
			p.Init();
		}
	}

	public void Completed(QuestID sentId) 
	{
		if (_index > _parts.Count - 1) return;
		if (_parts[_index].Id != sentId) return; 
		_parts[_index++].CompletePart();
	}

	public List<Part> Parts  
	{
		get 
		{
			return _parts;
		}
	}

}


[Serializable]
public class Part 
{
	[SerializeField] private bool _isComplete;
	[SerializeField] private QuestID _id;
	[SerializeField] private string _description;

	public void Init() 
	{
		_isComplete = false;
	}

	public void CompletePart() 
	{
		_isComplete = true;
	}

	public bool IsPartComplete => _isComplete;

	public QuestID Id => _id;
	public string Description => _description;

}
