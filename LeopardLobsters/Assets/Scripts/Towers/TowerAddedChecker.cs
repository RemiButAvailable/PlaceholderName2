using UnityEngine;
using UnityEngine.Events;

public class TowerAddedChecker : MonoBehaviour //checks when towers are placed and relays that to neighboorhoods and fountains
{
    public UnityEvent<BaseTower> towerEnter;
    public UnityEvent<BaseTower> towerExit;

    public void TowerEnter(BaseTower tower) {
        towerEnter.Invoke(tower);
        tower.Destroyed.AddListener(TowerExit);
    }
    public void TowerExit(BaseTower tower)
    {
        towerExit.Invoke(tower);
    }
}
