using UnityEngine;
using UnityEngine.Events;

class TutorialPopup : MonoBehaviour
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

    [SerializeField] float towerOffset;
    public void startAtBaseTower()
    {
        transform.position = TutorialHolder.curTower.transform.position + (Vector3)Vector2.up * towerOffset;
    }

    public void nextWhenTowerSelected() {
        TutorialHolder.curTower.Selected.AddListener(Next);
    }
    public void nextWhenTowerActive()
    {
        TutorialHolder.curTower.Activated.AddListener(Next);
    }


}