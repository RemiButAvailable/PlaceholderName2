using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NonUIButton : MonoBehaviour, IPointerDownHandler
{

    public AudioSource ButtonSound;
    public UnityEvent OnClick;
    Animation changeColor;
    bool active = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!active) {return; }

        OnClick?.Invoke();
        changeColor?.Play();
        if (ButtonSound) ButtonSound.Play();
    }

    public void Disable() {
        active = false;
    }
    public void Enable()
    {
        active = true;
    }
}
