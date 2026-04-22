using System;
using UnityEngine;

public class PickupInteraction : IInteractionAction, ITickableModule, ITriggerModule, IInteractionSignalSource
{
	private readonly Transform _ownerTransform;
	private readonly Rigidbody _rb;
	
	private Transform _holdPoint;
	private bool _isPickedUp;
	private bool _canBePickedUp = true;

	private readonly float _followSpeed;
	private readonly float _linearDamping;
	private readonly bool _disablePickupOnPlacement;

    public event Action<InteractionSignal> OnRaise;

    private PlaceableSlot _currentSlot;

    public PickableType PickableType { get; }

    public PickupInteraction(GameObject owner, PickupModuleConfigSO config)
	{
		_rb = owner.GetComponent<Rigidbody>();
		_ownerTransform = owner.transform;
		
		_followSpeed = config.followSpeed;
		_linearDamping = config.linearDamping;
		_disablePickupOnPlacement = config.disablePickupOnPlacement;
	}

	public void SetPickupState(bool state)
	{
		_canBePickedUp = !state;
	}

	public void Interact(Transform holdPoint)
	{
		if (!_canBePickedUp) return;

		if (!_isPickedUp)
		{
			PickUp(holdPoint);
		}
		else
		{
			Drop();
		}
	}

	private void PickUp(Transform holdPoint)
	{
		_ownerTransform.SetParent(null);
		_holdPoint = holdPoint;
		_isPickedUp = true;

		_rb.useGravity = false;
		_rb.linearDamping = _linearDamping;

		OnRaise?.Invoke(new InteractionSignal { InteractionAction = InteractionSignalType.PickedUp });
	}

	private void Drop()
	{
		_isPickedUp = false;
		_holdPoint = null;

		_rb.useGravity = true;
		_rb.linearDamping = 0f;
		
		// Try find a place
		if (_currentSlot != null) // Place found
		{
			_canBePickedUp = !_disablePickupOnPlacement;
			OnRaise?.Invoke(new InteractionSignal { InteractionAction = InteractionSignalType.Placed });
		}
		else // No place found
		{
			OnRaise?.Invoke(new InteractionSignal { InteractionAction = InteractionSignalType.Dropped });
		}
	}

	public void Tick()
    {
        if (!_isPickedUp || _holdPoint == null) return;
		
		Vector3 direction = _holdPoint.position - _rb.position;
		_rb.linearVelocity = direction * _followSpeed;
		
    }

    #region Placemenet logic
    public void OnTriggerEnterContext(Collider other)
    {
        if (other.TryGetComponent(out PlaceableSlot slot))
		{
			_currentSlot = slot;
		}
    }

    public void OnTriggerExitContext(Collider other)
    {
        if (other.TryGetComponent(out PlaceableSlot slot) && slot == _currentSlot)
		{
			if (_currentSlot == slot) _currentSlot = null;
		}
    }
    #endregion
}