using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "ScriptableObject/Quest")]
public class Quest : ScriptableObject
{
	[SerializeField] private List<QuestPart> _parts;
    [SerializeField] private TerminalState _terminalState;
    private int _index;
    

	public void Init() 
	{
		_index = 0;
		foreach (QuestPart p in _parts) 
		{
			p.Init();
		}
	}

	public void Completed(QuestID sentId) 
	{
		if (_index > _parts.Count - 1) return;
		if (_parts[_index].Id != sentId) return; 
		_parts[_index].TryCompletePart();
		if (_parts[_index].IsPartComplete) 
		{
			_index++;
		}
	}

	public bool IsComplete 
	{
		get 
		{
			return _index >= _parts.Count;
		}
	}

	public List<QuestPart> Parts  
	{
		get 
		{
			return _parts;
		}
	}

    public TerminalState MachineState => _terminalState;

}


/// <summary>
/// <c>Part</c> is a part of a quest and is used for making a quest have multiple objectives.
/// </summary>
[Serializable]
public class QuestPart
{
    [SerializeField] private QuestID _id;
    [SerializeField] private bool _isComplete;
    [SerializeField] private int _howManySteps = 0;
    [SerializeField] private string _description;
    private int _stepIndex = 1; // This needs to be one to make the times match the number of steps.

    /// <summary>
    /// Initialize the class this will set the completed stage to false.
    /// </summary>
    public void Init()
    {
        _isComplete = false;
        _stepIndex = 1;
    }

    /// <summary>
    /// Tries to complete a <c>Part</c> if this is doable.
    /// </summary>
    /// <returns><c>True</c> if the part could be completed else it returns <c>False</c></returns>
    public bool TryCompletePart()
    {
        if (_stepIndex < _howManySteps)
        {
            _stepIndex++;
            return false;
        }

        _isComplete = true;
        return true;
    }

    /// <summary>
    /// Is the part complete.
    /// </summary>
    public bool IsPartComplete => _isComplete;

    /// <summary>
    /// The Id that the part has.
    /// </summary>
    public QuestID Id => _id;

    /// <summary>
    /// If the <c>Part</c> has a step count it will return that with the description, else just the description.
    /// </summary>
    /// <returns>The amount of steps plus the description, if it has any steps. Else just the description.</returns>
    public override string ToString()
    {
        if (_howManySteps > 0) return _howManySteps.ToString() + " " + _description;

        return _description;
    }
}