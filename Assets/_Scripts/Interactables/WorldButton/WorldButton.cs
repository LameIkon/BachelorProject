using UnityEngine;

public class WorldButton : IInteractionAction
{
	private readonly ButtonData _buttonData;
    private readonly ButtonEventSO _onButtonEvent;
    private readonly MeshRenderer _lightIndicator;

	// Animations
	private readonly Animator _animator;
	private readonly int _animInt = Animator.StringToHash("PhysicalButton");

	public WorldButton(GameObject owner, BottonModuleConfigSO config, ButtonInteractionIdentitySO buttonDefinition)
	{
		_buttonData = buttonDefinition.buttonData;
		_onButtonEvent = buttonDefinition.buttonEvent;

		_lightIndicator = owner.GetComponentInChildren<MeshRenderer>();
		_animator = owner.GetComponent<Animator>();

		_buttonData?.SetColor(false);
		SetColorIndicator(_buttonData.Color);
	}

    public void Interact(Transform transform)
    {
		_buttonData?.SetColor(true);
		SetColorIndicator(_buttonData.Color);

		_animator.Play(_animInt);

		_onButtonEvent?.Raise(_buttonData.Type);
	}

	private void SetColorIndicator(Color color) 
	{
		_lightIndicator.material.color = color;
	}
}
