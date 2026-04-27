using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] EventTrigger trigger;
    public void Disable()
    {
        if(button) button.enabled = false;
        if(trigger) trigger.enabled = false;
    }
    public void Enable(){
        if (button) button.enabled = true;
        if (trigger) trigger.enabled = true;
    }
}
