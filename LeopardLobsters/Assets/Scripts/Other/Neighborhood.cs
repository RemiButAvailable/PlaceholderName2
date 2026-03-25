using System;
using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    //[SerializeField]
    //Dictionary<TowerType, float> towerPreference = new Dictionary<TowerType, float>();

    [SerializeField] List<TowerType> typeIndex = new List<TowerType>();
    [SerializeField] List<float> typeImpact = new List<float>(); //change later with a better solution like a serializeable dictionary
    List<BaseTower> towers = new List<BaseTower>();

    [SerializeField]
    float happinessPerTower;

    float curHappinessChange = 0;

    private void Start()
    {
        WaveCode.self.waveStarted.AddListener(resetCalculations);
    }

    public void towerEnter(BaseTower tower)
    {
        tower.Destroyed.AddListener(towerLeft);
        towers.Add(tower);

        //can add sfx vfx
        
        //tower type change
        if (typeIndex.Contains(tower.type)) 
            curHappinessChange += typeImpact[typeIndex.IndexOf(tower.type)];

        //distance to center calculation
        float dist = (tower.transform.position-transform.position).magnitude;
        dist = ((dist) / (1 + (1 / 3) * dist));

        Happiness_ManagerScript.self.CalculateHappiness(happinessPerTower + dist);
    }

    void towerLeft(BaseTower tower) {
        towers.Remove(tower);

        //tower type addition
        if (typeIndex.Contains(tower.type))
            curHappinessChange -= typeImpact[typeIndex.IndexOf(tower.type)];

        //distance to center calculation
        float dist = (tower.transform.position - transform.position).magnitude;
        dist = ((dist) / (1 + (1 / 3) * dist));

        curHappinessChange -= happinessPerTower + dist;
    }

    void resetCalculations() { //do after every wave just in case
        curHappinessChange = 0;
        List<BaseTower> tempTowers = towers;
        towers = new List<BaseTower>();
        foreach (BaseTower tower in tempTowers) {
            towerEnter(tower);
        }
    }
    
    

}

