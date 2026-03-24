using System;
using System.Collections.Generic;
using UnityEngine;

public class TestNeighborhood : MonoBehaviour
{
    [SerializeField]
    Dictionary<TowerType, float> towerPreference;

    [SerializeField]
    float happinessPerTower;

    float curHappinessChange = 0;

    public void towerEnter(BaseTower tower)
    {
        tower.Destroyed.AddListener(towerLeft);

        //can add sfx vfx

        if (towerPreference.ContainsKey(tower.type)) curHappinessChange += towerPreference[tower.type];
        curHappinessChange += happinessPerTower;
    }

    void towerLeft(TowerType type) {
        if (towerPreference.ContainsKey(type)) curHappinessChange -= towerPreference[type];
        curHappinessChange -= happinessPerTower;
    }


    
    [SerializeField]
    float timer;
    [SerializeField]
    float timerMax = .1f;

    private void FixedUpdate()
    {
        if (timer > 0) { timer -= Time.deltaTime; }
        else {
            timer = timerMax -= timer;
            //TestHappyManager.self.ChangeHappy(curHappinessChange);
        }
    }

}

