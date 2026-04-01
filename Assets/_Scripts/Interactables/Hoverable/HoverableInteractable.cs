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
