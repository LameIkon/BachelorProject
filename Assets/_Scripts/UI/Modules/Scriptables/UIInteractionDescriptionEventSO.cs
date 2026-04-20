using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="UI Interaction description Event SO", menuName = "ScriptableObject/Events/UI/Interaction Description data")]
public class UIInteractionDescriptionEventSO : ScriptableObject
{
	[Header("Action reference")]
	[SerializeField] private InputActionReference actionRef;
	[SerializeField] private int bindingIndex;

	[Header("Description")]
	public string actionDescription;

	public string ActionSymbol => actionRef.action.GetBindingDisplayString(bindingIndex).ToUpper();
}