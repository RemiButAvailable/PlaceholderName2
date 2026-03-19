using System;
using UnityEngine;

[Serializable]
public class FloatColor : IComparable<FloatColor>
{
    [SerializeField]
    public float percent;
    [SerializeField]
    public Color color;

    public int CompareTo(FloatColor other)
    {
        if (other == null) return 1;
        return this.percent.CompareTo(other.percent);
    }
}

