using UnityEngine;
using UnityEngine.Events;

class TutorialPopup: MonoBehaviour
{
    public UnityEvent next;
    public UnityEvent started;

    public void Next()
    {
        next.Invoke();
    }

    public UnityEvent<TutorialPopup> back;

    public void Back(TutorialPopup prev) { //when no tutorial popup is put in it automatically goes back one
        back.Invoke(prev);
    }
}