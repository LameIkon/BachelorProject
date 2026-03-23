using UnityEngine;
using System;
using System.Collections;

public sealed class PhysicalButton : MonoBehaviour, IInteractable
{

    [SerializeField] private ButtonData _buttonData;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] private MeshRenderer _lightIndicator;

	#region Unity Methods

	private void Start() 
	{
		_lightIndicator = GetComponentInChildren<MeshRenderer>();
		_buttonData.SetColor(false);
		SetColorIndicator(_buttonData.Color);
	}

	#endregion

	#region Interactable interface

	/// <summary>
	/// The button invokes an event with the button type currently assigned to it.
	/// </summary>
	public void Interact(Transform transform)
    {
		_buttonData.SetColor(true);
		SetColorIndicator(_buttonData.Color);
		StartCoroutine(PressButton());

        _onButtonEvent.Raise(_buttonData.Type);
    }

	#endregion

	public void SetColorIndicator(Color color) 
	{
		_lightIndicator.material.color = color;
	}

	public IEnumerator PressButton() 
	{
		Vector3 pos = transform.forward * .02f;
		pos.x = pos.z;
		pos.z = -pos.y;
		pos.y = 0;

		Vector3 oldPosition = transform.position;
		Vector3 newPosition = transform.position;
		newPosition += pos;
		Debug.Log(pos);

		for (float i = 0; i < 1; i += .01f)
		{
			transform.position = Vector3.Lerp(oldPosition, newPosition, i);
			yield return null;
		}

		for (float i = 0; i < 1; i += .01f)
		{
			transform.position = Vector3.Lerp(newPosition, oldPosition, i);
			yield return null;
		}

	}
}


