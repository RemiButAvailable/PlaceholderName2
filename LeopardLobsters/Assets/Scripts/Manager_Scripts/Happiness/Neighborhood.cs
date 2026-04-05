using System;
using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    //[SerializeField]
    //Dictionary<TowerType, float> towerPreference = new Dictionary<TowerType, float>();

    [SerializeField] List<TowerType> typeIndex = new List<TowerType>();
    [SerializeField] List<float> typeMult = new List<float>(); //change later with a better solution like a serializeable dictionary
    List<BaseTower> towers = new List<BaseTower>();

    [SerializeField] Collider2D neighboorhoodCenter;
    [SerializeField] TowerAddedChecker checker;

    [SerializeField]
    float happinessPerTower;

    public float curHappinessChange = 0;

    //Animation + sfx
    [SerializeField] Animator animator;
    [SerializeField] GameObject animatorParent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip happyUp;
    [SerializeField] AudioClip happyDown;

    private void Start()
    {
        checker.towerEnter.AddListener(towerEnter);
        checker.towerExit.AddListener(towerLeft);

        Happiness_ManagerScript.self.Neighborhoods.Add(this);
        WaveCode.self.waveStarted.AddListener(resetCalculations);

        animatorParent.transform.position = neighboorhoodCenter.bounds.center;
    }
    public float calcTower(BaseTower tower)
    {
        float happinessChange = 0;

        tower.Destroyed.AddListener(towerLeft);
        towers.Add(tower);

        happinessChange += happinessPerTower;

        //tower type mult
        if (typeIndex.Contains(tower.type))
            happinessChange *= typeMult[typeIndex.IndexOf(tower.type)];

        //distance to closest point on collider calculation
        float dist = (tower.transform.position - tower.GetClosestPointOnCollider(neighboorhoodCenter)).magnitude;
        dist = (1 / (1 + dist / 3));
        happinessChange *= dist;

        curHappinessChange += happinessChange;

        return happinessChange;
    }

    public void towerEnter(BaseTower tower)
    {
        float happinessChange = calcTower(tower);

        //sfx vfx
        if (happinessChange > 0) {
            animator.Play("HappinessUp");
            audioSource.clip = happyUp;
            audioSource.Play();
        }
        if (happinessChange < 0)
        {
            animator.Play("HappinessDown");
            audioSource.clip = happyDown;
            audioSource.Play();
        }

    }

    void towerLeft(BaseTower tower) {
        towers.Remove(tower);

        float happinessChange = 0;

        happinessChange += happinessPerTower;

        //tower type mult
        if (typeIndex.Contains(tower.type))
            happinessChange *= typeMult[typeIndex.IndexOf(tower.type)];

        //distance to closest point on collider calculation
        float dist = (tower.transform.position - tower.GetClosestPointOnCollider(neighboorhoodCenter)).magnitude;
        dist = (1 / (1 + dist / 3));
        happinessChange *= dist;

        curHappinessChange -= happinessChange;
    }

    void resetCalculations() { //do after every wave just in case
        curHappinessChange = 0;
        List<BaseTower> tempTowers = towers;
        towers = new List<BaseTower>();
        foreach (BaseTower tower in tempTowers) {
            calcTower(tower);
        }
    }
    


}

