using UnityEngine;

public abstract class HoverableInteractable : MonoBehaviour, IHoverable
{
	[Header("Interaction Menu")]
    [SerializeField] protected InteractionMenuContent _interactionMenu;

    protected HighlightHandler _highlightHandler;

	protected virtual void Awake()
	{
		_highlightHandler = new HighlightHandler(this.gameObject);
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

public class HoverModule
{
    private readonly GameObject _owner;
    private readonly HighlightHandler _highlightHandler;

    public HoverModule(GameObject owner)
    {
        _owner = owner;
        _highlightHandler = new HighlightHandler(_owner);
    }

    public void OnHoverEnter()
    {
        _highlightHandler?.SetHighlight(true);
    }

    public void OnHoverExit()
    {
        _highlightHandler?.SetHighlight(false);
    }
}

public class PickupModule
{
    // TBD
}

public class ButtonModule
{
    // TBD
}

public class InteractionMenuModule
{
    // TBD
}
