using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public float happiness;
    public List<Neighborhood> Neighborhoods = new List<Neighborhood>();
    
    public HappinessBar barHappyUI;
    static public Happiness_ManagerScript self;



    private void Awake()
    {
        self = this;
    }
    void Start()
    {
        
        barHappyUI.ChangeBar(happiness);
    }

    [SerializeField] float timerMax = .1f;
    [SerializeField] float timer = 0;
    void FixedUpdate()
    {
        if(WaveCode.self.WaveStart)
        {
            timer-=Time.deltaTime;

            if (timer <= 0)
            {
                timer = timerMax + timer;

                foreach (Neighborhood hood in Neighborhoods)
                {
                    happiness += hood.curHappinessChange;
                }

                if (happiness > 1)
                {
                    happiness = 1;
                }
                if (happiness <= 0)
                {
                    //Game Lose Stuff
                    //VFX SFX
                    SceneManager.LoadScene("HappyLoseScreen");
                }

                barHappyUI.ChangeBar(happiness);
            }
        }
    }


    public void ChangeHappy(float amount)
    {
        Debug.Log("amount is " + amount);
        happiness += amount;
    }

    /*
     *     public float convertedToPercentHappiness;
    public Collider2D[] neighborhoods;
    public Collider2D[] neighborhoodGreaterAreas;
    public float[] distances;
    float impactOnHappiness;
    NeighborhoodScript neighborhoodScript;
    public WaveCode waveCode;
    public GameObject waveManager;
    public void CalculateHappiness(GameObject building)
    {
        for (int i = 0; i < neighborhoods.Length; i++)
        {
            bool overlapping = building.GetComponent<Collider2D>().bounds.Intersects(neighborhoodGreaterAreas[i].bounds);
            if (overlapping)
            {
                distances[i] = 5 - Vector3.Distance(neighborhoods[i].ClosestPoint(building.transform.position), building.transform.position);
                float distImpact = (distances[i]) / (1 + (1/3) * distances[i]);
                Debug.Log("dist impact = " + distImpact);
                if (building.tag == "archerTower")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness += distImpact + neighborhoodScript.archerTowerSensitivity;
                }
                else if(building.tag == "soldierTower")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness += distImpact + neighborhoodScript.soldierTowerSensitivity;
                }
                else if(building.tag == "fountain")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness -= distImpact - neighborhoodScript.fountainSensitivity;
                }
            }
        }
        happinessROC += impactOnHappiness;
        Debug.Log("happinessROC = " + happinessROC);
        // dont forget to call barHappyUI.ChangeBar(percent) to new happiness
    }
    */

    /*IEnumerator changeHappiness()
    {
        while(true)
        {
            if (WaveCode.self.WaveStart)
            {
                happiness -= happinessROC;
                convertedToPercentHappiness = 1 / (happiness);
                if (convertedToPercentHappiness > 1)
                {
                    convertedToPercentHappiness = 1;
                }
                barHappyUI.ChangeBar(convertedToPercentHappiness);
                tempHappinessText.text = "happiness = " + convertedToPercentHappiness + " happiness rate of change = " + happinessROC;
            }
            yield return new WaitForSeconds(1);
        }
    }*/
}
