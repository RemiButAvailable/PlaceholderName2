using TMPro;
using UnityEngine;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Collider2D[] neighborhoods;
    public Collider2D[] neighborhoodGreaterAreas;
    public float[] distances;
    float impactOnHappiness;
    public float happiness;
    public float happinessROC;
    NeighborhoodScript neighborhoodScript;
    public WaveCode waveCode;
    public GameObject waveManager;
    public HappinessBar barHappyUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveCode = waveManager.GetComponent<WaveCode>();
        distances = new float[neighborhoods.Length];
        barHappyUI.ChangeBar(happiness);
    }

    // Update is called once per frame
    void Update()
    {
        if(waveCode.WaveStart)
        {
            happiness += happinessROC;
        } 
    }
    public void CalculateHappiness(GameObject building)
    {
        for (int i = 0; i < neighborhoods.Length; i++)
        {
            if (building.GetComponent<Collider2D>().IsTouching(neighborhoodGreaterAreas[i]))
            {
                distances[i] = Vector3.Distance(neighborhoods[i].ClosestPoint(building.transform.position), building.transform.position);
                float distImpact = (distances[i]) / (1 + distances[i]);
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
                    impactOnHappiness += distImpact + neighborhoodScript.fountainSensitivity;
                }
            }
        }
        happinessROC += impactOnHappiness;
        // dont forget to call barHappyUI.ChangeBar(percent) to new happiness
    }

}
