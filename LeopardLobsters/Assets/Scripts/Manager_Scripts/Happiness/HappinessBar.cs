/* Description: The happiness bar that will show at what point of happiness it is.
 * Showing really happy, happy, medium, upset and angry.
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Image bar;
    public FloatColor[] colorChanges;
    public Image icon;
    int index = 0;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip happyUp;
    [SerializeField] AudioClip happyDown;

    private void Start()
    {
        float num = Happiness_ManagerScript.self.happiness;
        GetIndex(num);
        ChangeBar(num);
        Array.Sort(colorChanges);

    }

    //changes color and updates index stuff when called by happiness manager
    public void ChangeBar(float percent) {
        int oldIndex = index;
        
        bar.fillAmount = percent;
        GetIndex(percent);
        bar.color = colorChanges[index].color;

        if (!source.isPlaying) {
            if (oldIndex > index)
            {
                source.clip = happyDown;
                source.Play();
            }
            else if(oldIndex < index)
            {
                source.clip = happyUp;
                source.Play();
            }
        }
        //if(colorChanges[index].sprite) icon.sprite = colorChanges[index].sprite; // old
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

