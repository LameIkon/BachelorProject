using UnityEngine;

public class WorldToggleButton : IInteractionAction
{
	private readonly ButtonData _buttonData;
    private readonly ButtonEventSO _onButtonEvent;
	private readonly AudioSource _audioSource;
	private readonly ToggleButtonModuleConfigSO _config;

	// Animations
	private readonly Animator _animator;
	private readonly int _animInInt;
	private readonly int _animOutInt;

	// State
	private bool _isPressed;

	public WorldToggleButton(GameObject owner, ToggleButtonModuleConfigSO config, ToggleButtonInteractionIdentitySO identity, AudioSource source)
	{
		_buttonData = identity.buttonData;
		_onButtonEvent = identity.buttonEvent;
		_animInInt = Animator.StringToHash(identity.pressInanimation.name);
		_animOutInt = Animator.StringToHash(identity.pressOutanimation.name);
		_audioSource = source;
		_config = config;

		_animator = owner.GetComponent<Animator>();

		_buttonData?.SetColor(false);
		_isPressed = false;
	}

    public void Interact(Transform transform)
    {
		if (!_isPressed)
		{
			_animator.Play(_animInInt);
			_isPressed = true;
		}
		else
		{
			_animator.Play(_animOutInt);
			_isPressed = false;
		}

		// Change this later maybe idk Troels look at this
		_buttonData?.SetColor(true);
		_onButtonEvent?.Raise(_buttonData.Type);
		_config.PlaySound(_audioSource);
	}
}