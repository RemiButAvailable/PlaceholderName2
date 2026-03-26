using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NonUIButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnClick;
    Animation changeColor;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        print("clicked "+gameObject);
        OnClick?.Invoke();
        changeColor?.Play();
    }
}
