using UnityEngine;
using UnityEngine.Events;

public class TowerAddedChecker : MonoBehaviour //checks when towers are placed and relays that to neighboorhoods and fountains
{
    UnityEvent<BaseTower> towerEnter;
    UnityEvent<BaseTower> towerExit;

    public void TowerEnter(BaseTower tower) {
        towerEnter.Invoke(tower);
    }
    public void TowerExit(BaseTower tower)
    {
        towerExit.Invoke(tower);
    }
}
