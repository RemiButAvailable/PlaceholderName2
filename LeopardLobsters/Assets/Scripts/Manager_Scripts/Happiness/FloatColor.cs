using System;
using UnityEngine;

[Serializable]
public class FloatColor : IComparable<FloatColor> //can be further expanded with animations or sounds
{
    [SerializeField]
    public float percent;
    [SerializeField]
    public Color color;
    [SerializeField]
    public Sprite sprite;

    public int CompareTo(FloatColor other)
    {
        if (other == null) return 1;
        return this.percent.CompareTo(other.percent);
    }
}

