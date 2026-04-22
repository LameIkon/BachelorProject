using UnityEngine;

public class PlacementModule
{
    private IPickable _pickable;
    private PlaceableSlot _currentSlot;

    public PlacementModule(IPickable pickable)
	{
		_pickable = pickable;
	}

    public void SetSlot(PlaceableSlot slot)
    {
        _currentSlot = slot;
    }

    //public bool TryPlace()
    //{
    //    if (_currentSlot == null) return false;

    //    return _currentSlot.TryPlace(_pickable);
    //}
}