using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Object", menuName = "ScriptableObject/Quest")]
public class Quest : ScriptableObject
{
	private bool _isComplete = false;
	[SerializeField] private string _title;
	[SerializeField] private string _description;

	public bool IsComplete {
		get {return _isComplete;}
		set { _isComplete = value;}
	}
	public string Title { get { return _title; } }
	public string Description { get { return _description; } }

}