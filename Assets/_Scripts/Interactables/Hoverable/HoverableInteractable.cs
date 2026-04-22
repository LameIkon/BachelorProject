using UnityEngine;

public abstract class HoverableInteractable : MonoBehaviour, IHoverable
{
	[Header("Interaction Menu")]
    [SerializeField] protected InteractionMenuContent _interactionMenu;

    protected HighlightHandler _highlightHandler;

	protected virtual void Awake()
	{
		//_highlightHandler = new HighlightHandler(this.gameObject);
		_interactionMenu?.Initialize();
	}

    public virtual void OnHoverEnter()
    {
		_interactionMenu?.InteractionMenuHandler?.OnHoverState(true);
		_highlightHandler?.SetHighlight(true);
    }

    public virtual void OnHoverExit()
    {
		_interactionMenu?.InteractionMenuHandler?.OnHoverState(false);
		_highlightHandler?.SetHighlight(false);
    }

	protected virtual void OnDestroy()
    {
        _interactionMenu?.InteractionMenuHandler?.Dispose();
    }
}

public class InteractionGuideModule
{
    // Display interaction option to ActionGuideUI
    private readonly string _actionIcon;
    private readonly string _actionDescription;

    public InteractionGuideModule(string actionIcon, string actionDescription)
    {
        _actionIcon = actionIcon;
        _actionDescription = actionDescription;
    }

    public InteractionData GetInteractionData()
    {
		InteractionData data = new InteractionData
		{
			icon = _actionIcon,
			description = _actionDescription,
		};

        return data;
    }
}

public class ButtonModule
{
	private readonly ButtonData _buttonData;
	private readonly MeshRenderer _lightIndicator;

	private readonly Animator _animator;
	private readonly int _animInt = Animator.StringToHash("PhysicalButton");

	private readonly ButtonEventSO _onButtonEvent;

	public ButtonModule(GameObject owner, ButtonData data, ButtonEventSO buttonEvent)
	{
		_buttonData = data;
		_onButtonEvent = buttonEvent;

		_lightIndicator = owner.GetComponentInChildren<MeshRenderer>();
		_animator = owner.GetComponent<Animator>();

		_buttonData?.SetColor(false);
		SetColorIndicator(_buttonData.Color);
	}



	public void PressButton()
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

public class LeverModule
{
	private readonly ButtonData _leverData;
	private readonly ButtonEventSO _onLeverEvent;

	public LeverModule(ButtonData data, ButtonEventSO leverEvent)
	{
		_onLeverEvent = leverEvent;
	}

	public void UseLever()
	{
		//TBD
		_onLeverEvent?.Raise(_leverData.Type);
	}
}

public class DoorModule
{

}

public class InteractionMenuModule
{
	private readonly InteractionMenuContent _interactionMenu;
    private readonly HighlightHandler _highlightHandler;

}
