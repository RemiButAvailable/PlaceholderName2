using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Image bar;
    public Color[] colors; //colors changes color to index when points in bar of index is reached
    public float[] pointsInBar; //needs to be in order small -> large
    public int index = 0;
    float progress => bar.fillAmount;

    private void Start()
    {

    }

}
