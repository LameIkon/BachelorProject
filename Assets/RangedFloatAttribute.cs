using UnityEngine;
using System;

[Serializable]
public class RangedFloatAttribute : PropertyAttribute
{
    public enum RangeDisplayType 
    {
        LockedRanges,
        EditableRanges,
        HideRanges
    }
    public float max;
    public float min;
    public RangeDisplayType rangeDisplayType;

    public RangedFloatAttribute(float min, float max, RangeDisplayType rangeDisplayType = RangeDisplayType.LockedRanges)
    {
        this.min = min;
        this.max = max;
        this.rangeDisplayType = rangeDisplayType;
    }

}