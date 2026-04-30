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
    BaseTower curTower => TutorialHolder.curTower;
    public void startAtBaseTower()
    {
        transform.position = curTower.transform.position + (Vector3)Vector2.up * towerOffset;
    }

    public void nextWhenTowerSelected() {
        curTower.Selected.AddListener(Next);
    }
    public void nextWhenTowerActive()
    {
        curTower.Activated.AddListener(Next);
    }
    public void nextWhenTowerRemove() {
        curTower.RemovedPeople.AddListener(nextWhenTowerRemovePt2);
    }
    void nextWhenTowerRemovePt2(){
        if (curTower.people == 0) Next();
    }


}