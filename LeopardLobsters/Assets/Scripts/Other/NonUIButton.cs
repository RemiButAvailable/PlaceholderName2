using UnityEngine;
using UnityEngine.Events;

public class NonUIButton : MonoBehaviour
{
    public UnityEvent OnClick;
    Animation changeColor;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
        changeColor?.Play();
    }
}
