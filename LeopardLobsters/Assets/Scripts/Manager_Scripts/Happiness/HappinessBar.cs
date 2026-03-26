using System;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Image bar;
    public FloatColor[] colorChanges;
    public Image icon;
    int index = 0;

    private void Start()
    {
        Array.Sort(colorChanges);

    }

    //changes color and updates index stuff when called by happiness manager
    public void ChangeBar(float percent) {
        bar.fillAmount = percent;
        GetIndex(percent);
        bar.color = colorChanges[index].color;
        if(colorChanges[index].sprite) icon.sprite = colorChanges[index].sprite;
    }

    //goes up or down the array until it hits the right thing
    void GetIndex(float percent) {
        if (percent > colorChanges[index].percent)
        {
            if (index + 1 >= colorChanges.Length) { index = colorChanges.Length - 1; return; }
            if (percent < colorChanges[index + 1].percent) { return; }
            index++;
            GetIndex(percent);
        }
        else if (percent < colorChanges[index].percent)
        {
            if (index - 1 < 0) { index = 0; return; }
            index--;
            GetIndex(percent);
        }
    }
        
}

