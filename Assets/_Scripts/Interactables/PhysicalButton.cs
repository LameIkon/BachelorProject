using UnityEngine;
using System.Collections;

public sealed class PhysicalButton : HoverableInteractable, IInteractable 
{
	[Header("Button Settings")]
    [SerializeField] private ButtonData _buttonData;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] private MeshRenderer _lightIndicator;
	[SerializeField] private float _buttonPressSpeed;
	[SerializeField] private bool _isLever;
	private bool _isPressed;

    private Vector3 oldPosition;
	private Vector3 newPosition;


    #region Unity Methods

    private void Start() 
	{
		_lightIndicator = GetComponentInChildren<MeshRenderer>();
		_buttonData?.SetColor(false);
		SetColorIndicator(_buttonData.Color);
		_buttonPressSpeed = .1f;

        Vector3 pos = transform.forward * .02f;
        pos.x = pos.z;
        pos.z = -pos.y;
        pos.y = 0;

        oldPosition = transform.position;
        newPosition = transform.position;
        newPosition += pos;
        Debug.Log(pos);
    }

	#endregion

	#region Interactable interface

	/// <summary>
	/// The button invokes an event with the button type currently assigned to it.
	/// </summary>
	public void Interact(Transform transform)
    {
		//Debug.Log("interact with button");
		_buttonData?.SetColor(true);
		SetColorIndicator(_buttonData.Color);
		if (!_isLever)
		{
			StartCoroutine(PressButton());
		}
		else
		{
			StartCoroutine(PullLever());	
		}

		_onButtonEvent?.Raise(_buttonData.Type);
    }

	#endregion

	public void SetColorIndicator(Color color) 
	{
		_lightIndicator.material.color = color;
	}

	public IEnumerator PressButton() 
	{

		for (float i = 0; i < 1; i += _buttonPressSpeed)
		{
			transform.position = Vector3.Lerp(oldPosition, newPosition, i);
			yield return null;
		}

		for (float i = 0; i < 1; i += _buttonPressSpeed)
		{
			transform.position = Vector3.Lerp(newPosition, oldPosition, i);
			yield return null;
		}

	}

	public IEnumerator PullLever()
	{
		if (!_isPressed) 
		{
			for (float i = 0; i < 1; i += _buttonPressSpeed)
			{
				transform.position = Vector3.Lerp(oldPosition, newPosition, i);
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i < 1; i += _buttonPressSpeed)
			{
				transform.position = Vector3.Lerp(newPosition, oldPosition, i);
				yield return null;
			}
		}

		_isPressed = !_isPressed;

	}
}
