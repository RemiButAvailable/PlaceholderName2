using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    BaseTower tower;

    public void towerSelect(BaseTower tower) {
        this.tower = tower;
    }

    //connected in inspector
    public void Sell() {
        if (!tower) return;
        tower.Sell();
    }
    public void Add()
    {
        if (!tower) return;
        tower.AddPeople();
    }
    public void Remove()
    {
        if (!tower) return;
        tower.RemovePeople();
    }

}
