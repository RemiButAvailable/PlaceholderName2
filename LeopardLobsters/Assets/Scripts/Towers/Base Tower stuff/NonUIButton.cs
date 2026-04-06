using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NonUIButton : MonoBehaviour, IPointerDownHandler
{

    public AudioSource ButtonSound;
    public UnityEvent OnClick;
    Animation changeColor;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
        changeColor?.Play();
        if (ButtonSound) ButtonSound.Play();
    }
}
