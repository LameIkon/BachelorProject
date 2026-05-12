using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "ScriptableObject/Quest")]
public class Quest : ScriptableObject
{
	[SerializeField] private List<QuestPart> _parts;
    [SerializeField] private string _questCompleteDescription;
    [SerializeField] private List<TerminalAndButton> _terminalBehavior;
    [SerializeField] private bool _setNewQuestOnComplete;
    private int _curentQuestPartIndex;
    
    // Getters
    public string QuestCompleteDescription => _questCompleteDescription;
    public bool SetNewQuestOnComplete => _setNewQuestOnComplete;

	public void Init() 
	{
		_curentQuestPartIndex = 0;
		foreach (QuestPart p in _parts) 
		{
			p.Init();
		}
	}

	public bool Completed(QuestID sentId) 
	{
		if (_curentQuestPartIndex > _parts.Count - 1) return false;
		if (_parts[_curentQuestPartIndex].Id != sentId) return false; 
		_parts[_curentQuestPartIndex].TryCompletePart();
		if (_parts[_curentQuestPartIndex].IsPartComplete) 
		{
			_curentQuestPartIndex++;
            return true;
		}
        return false;
	}

	public bool IsComplete 
	{
		get 
		{
			return _curentQuestPartIndex >= _parts.Count;
		}
	}

	public List<QuestPart> Parts  
	{
		get 
		{
			return _parts;
		}
	}

    public QuestPart CurrentQuestPart
    {
        get
        {
            if (_parts == null || _parts.Count == 0) return null;

            if (_curentQuestPartIndex < 0 || _curentQuestPartIndex >= _parts.Count) return null;

            return _parts[_curentQuestPartIndex];
        }
    }

    public List<TerminalAndButton> TerminalBehavior => _terminalBehavior;
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
    [SerializeField] private PickupInteractionIdentitySO _itemPickup;
    public bool hideQuestDescription;

    private string _replacer = "()";
    private string _replacer2 = "{}";
    //public bool ShowQuestPartOnlyOnSelection;

    [SerializeField] private LocalizedContentSO _content;

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
        string result = _description;
        if (_howManySteps > 0)
        {
            if (result.Contains(_replacer)) // Add step counter at _replacer location
            {
                result = result.Replace(_replacer, _howManySteps.ToString());
            }
            else // Add steps counter first
            {
                result = _howManySteps.ToString() + " " + _description;
            }
        }          
          
        if (_itemPickup != null)
        {
            if (result.Contains(_replacer2)) // Add objective name at _replacer2 location
            {
                result = result.Replace(_replacer2, _itemPickup.type.ToString());
            }
            else // Add objective name lastly
            {
                result = result + " " + _itemPickup.type.ToString(); 
            }
        }
            

        return result;
    }
}

[Serializable]
public class TerminalAndButton 
{
    public TerminalType TType;
    public ButtonType BType;
}