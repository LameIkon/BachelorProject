using UnityEngine;

public class WorldLever : IInteractionAction
{
	private readonly ButtonData _buttonData;
    private readonly ButtonEventSO _onButtonEvent;
	private readonly AudioSource _audioSource;
	private readonly LeverModuleConfigSO _config;

	// Animations
	private readonly Animator _animator;
	private readonly int _animPullLeftInt;
	private readonly int _animPullRightInt;

	// State
	private bool _isPulled;

	public WorldLever(GameObject owner, LeverModuleConfigSO config, LeverInteractionIdentitySO identity, AudioSource source)
	{
		_onButtonEvent = identity.buttonEvent;
		_buttonData = identity.buttonData;
		_animPullLeftInt = Animator.StringToHash(identity.pullLeftAnimation.name);
		_animPullRightInt = Animator.StringToHash(identity.pullRightAnimation.name);
		_audioSource = source;
		_config = config;

		_animator = owner.GetComponent<Animator>();

		_isPulled = false;
	}

    public void Interact(Transform transform)
    {
		if (!_isPulled) // Pull to right
		{
			_animator.Play(_animPullRightInt);
			_isPulled = true;

		}
		else // Pull to left
		{
			_animator.Play(_animPullLeftInt);
			_isPulled = false;
		}

		_onButtonEvent?.Raise(_buttonData.Type);
		_config.PlaySound(_audioSource);



	}
}
