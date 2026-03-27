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

    [SerializeField]Collider2D neighboorhoodCenter;
    [SerializeField] TowerAddedChecker checker;

    [SerializeField]
    float happinessPerTower;

    public float curHappinessChange = 0;

    private void Start()
    {
        //WaveCode.self.waveStarted.AddListener(resetCalculations);
        checker.towerEnter.AddListener(towerEnter);
        checker.towerExit.AddListener(towerLeft);
        Happiness_ManagerScript.self.Neighborhoods.Add(this);
    }

    public void towerEnter(BaseTower tower)
    {
        tower.Destroyed.AddListener(towerLeft);
        towers.Add(tower);

        //can add sfx vfx
        
        //tower type change
        if (typeIndex.Contains(tower.type)) 
            curHappinessChange += typeImpact[typeIndex.IndexOf(tower.type)];

        //distance to closest point on collider calculation
        float dist = (tower.transform.position - tower.GetClosestPointOnCollider(neighboorhoodCenter)).magnitude;
        dist = (1 / (1 + dist / 3));

        curHappinessChange += happinessPerTower * dist;
    }

    void towerLeft(BaseTower tower) {
        towers.Remove(tower);

        //tower type addition
        if (typeIndex.Contains(tower.type))
            curHappinessChange -= typeImpact[typeIndex.IndexOf(tower.type)];

        //distance to center calculation
        float dist = (tower.transform.position - tower.GetClosestPointOnCollider(neighboorhoodCenter)).magnitude;
        dist = (1 / (1 + dist / 3));

        curHappinessChange -= happinessPerTower * dist;
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

