using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NonUIButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnClick;
    Animation changeColor;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
        changeColor?.Play();
    }
}
