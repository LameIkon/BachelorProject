using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Prompt Data SO", menuName = "ScriptableObject/Interactable/Input Prompt")]
public class InputPromptDataSO : ScriptableObject
{
	[Header("Action reference")]
	[SerializeField] private InputActionReference actionRef;
	[SerializeField] private int bindingIndex;

	[Header("Description")]

	public string actionDescription;

	public string ActionSymbol => actionRef.action.GetBindingDisplayString(bindingIndex).ToUpper();
}
