using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;

public class Neighborhood : MonoBehaviour
{
    //[SerializeField]
    //Dictionary<TowerType, float> towerPreference = new Dictionary<TowerType, float>();

    [SerializeField] List<TowerType> typeIndex = new List<TowerType>();
    [SerializeField] List<float> typeMult = new List<float>(); //change later with a better solution like a serializeable dictionary
    List<BaseTower> towers = new List<BaseTower>();

    [Space]
    [SerializeField] Collider2D neighboorhoodCenter;
    [SerializeField] TowerAddedChecker checker;

    [Space]
    [SerializeField]
    float happinessPerTower;
    public float curHappinessChange = 0;

    [Space]
    //Animation + sfx
    [SerializeField] Animator animator;
    [SerializeField] GameObject animatorParent;

    [SerializeField] Sprite[] buildings;
    [SerializeField] int buildingTries;
    [SerializeField] Color[] colorBounds = new Color[2];

    [Space]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip happyUp;
    [SerializeField] AudioClip happyDown;

    private void Start()
    {
        checker.towerEnter.AddListener(towerEnter);
        checker.towerExit.AddListener(towerLeft);

        Happiness_ManagerScript.self.Neighborhoods.Add(this);
        WaveCode.self.waveStarted.AddListener(resetCalculations);

        Bounds bounds = neighboorhoodCenter.bounds;
        animatorParent.transform.position = bounds.center;

        for (int i = 0; i < buildingTries; i++) {
            Vector3 position = new Vector3(
                rand(bounds.min.x, bounds.max.x),
                rand(bounds.min.y, bounds.max.y),
                0);

            if (!neighboorhoodCenter.OverlapPoint(position)) continue;

            Color color = new Color(
                rand(colorBounds[0].r,colorBounds[1].r),
                rand(colorBounds[0].g, colorBounds[1].g),
                rand(colorBounds[0].b, colorBounds[1].b));

            Sprite sprite = buildings[rand(0,buildings.Length)];

            GameObject building = new GameObject("building");
            SpriteRenderer spriteR = building.AddComponent<SpriteRenderer>();
            building.transform.position = position;
            spriteR.sprite = sprite;
            spriteR.color = color;
            spriteR.spriteSortPoint = SpriteSortPoint.Pivot;
        }
        
    }

    int rand(int start, int end) { return Random.Range(start, end); }
    float rand(float start, float end) { return Random.Range(start, end); }

    public float calcTower(BaseTower tower)
    {
        float happinessChange = 0;

        towers.Add(tower);

        happinessChange += happinessPerTower;

        //tower type mult
        if (typeIndex.Contains(tower.type))
            happinessChange *= typeMult[typeIndex.IndexOf(tower.type)];

        curHappinessChange += happinessChange;

        return happinessChange;
    }
    // If a tower enters the neighborhood, change the happiness based on the tower.
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

