using UnityEngine;

public class WorldButton : IInteractionAction
{
	private readonly ButtonData _buttonData;
    private readonly ButtonEventSO _onButtonEvent;
	private readonly AudioSource _audioSource;
	private readonly ButtonModuleConfigSO _config;

	// Animations
	private readonly Animator _animator;
	private readonly int _animInt;

	public WorldButton(GameObject owner, ButtonModuleConfigSO config, ButtonInteractionIdentitySO identity, AudioSource source)
	{
		_buttonData = identity.buttonData;
		_onButtonEvent = identity.buttonEvent;
		_animInt = Animator.StringToHash(identity.animation.name);
		_audioSource = source;
		_config = config;

		_animator = owner.GetComponent<Animator>();

		_buttonData?.SetColor(false);
	}

    public void Interact(Transform transform)
    {
		_buttonData?.SetColor(true);

		_animator.Play(_animInt);

		_onButtonEvent?.Raise(_buttonData.Type);

		_config.PlaySound(_audioSource);
	}
}
