using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public sealed class PhysicalButton : HoverableInteractable
{
	[Header("Button Settings")]
    [SerializeField] private ButtonData _buttonData;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] private MeshRenderer _lightIndicator;
	[SerializeField] private float _buttonPressSpeed;
	[SerializeField] private bool _isLever;
	private bool _isPressed;
	private Animator _anim;
	private int _animInt = Animator.StringToHash("PhysicalButton");

    #region Unity Methods

    private void Start() 
	{
		_lightIndicator = GetComponentInChildren<MeshRenderer>();
		_buttonData?.SetColor(false);
		SetColorIndicator(_buttonData.Color);
		_anim = GetComponent<Animator>();
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
			PressButton();
		}
		else
		{
			PullLever();	
		}

		_onButtonEvent?.Raise(_buttonData.Type);
    }

	#endregion

	public void SetColorIndicator(Color color) 
	{
		_lightIndicator.material.color = color;
	}

	public void PressButton() 
	{
		_anim.Play(_animInt);
	}

	public void PullLever()
	{

		_isPressed = !_isPressed;

	}

    public InteractionData GetInteractionData()
    {
		InteractionData data = new InteractionData
		{
			icon = string.Empty,
			description = string.Empty,
		};

        return data;
    }
}
