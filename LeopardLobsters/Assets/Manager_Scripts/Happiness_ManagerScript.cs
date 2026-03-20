using UnityEngine;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Collider2D[] neighborhoods;
    public float[] distances;
    float impactOnHappiness;
    public float happiness;
    public float happinessROC;
    //NeighborhoodScript neighborhoodScript;
    public WaveCode waveCode;
    public GameObject waveManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveCode = waveManager.GetComponent<WaveCode>();
        distances = new float[neighborhoods.Length];
    }

    // Update is called once per frame
    void Update()
    {
        /*if(waveCode.waveHappening)
        {
            happiness += happinessROC;
        }*/ 
    }
    /*public void CalculateHappiness(GameObject building)
    {
        for (int i = 0; i < neighborhoods.Length; i++)
        {
            if (building.GetComponent<Collider2D>().IsTouching(neighborhoods[i]))
            {
                distances[i] = Vector3.Distance(neighborhoods[i].ClosestPoint(building.transform.position), building.transform.position);
                //int distImpact = distances[i] 
                if (building.tag == "archerTower")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness += distances[i] + neighborhoodScript.towerSensitivity;
                }
                else if(building.tag == "soldierTower")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness += distances[i] + neighborhoodScript.soldierSensitivity;
                }
                else if(building.tag == "fountain")
                {
                    neighborhoodScript = neighborhoods[i].gameObject.GetComponent<NeighborhoodScript>();
                    impactOnHappiness += distances[i] + neighborhoodScript.fountainSensitivity;
                }
                Debug.Log("impact on happiness = " + impactOnHappiness);
            }
        }
        happiness += impactOnHappiness;
    }*/
}
