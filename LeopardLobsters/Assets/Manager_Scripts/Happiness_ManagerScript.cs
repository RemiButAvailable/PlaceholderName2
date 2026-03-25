using TMPro;
using UnityEngine;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float happiness;
    public float happinessROC;
    
    public HappinessBar barHappyUI;
    static public Happiness_ManagerScript self;

    public TextMeshProUGUI tempHappinessText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = this;
        //waveCode = waveManager.GetComponent<WaveCode>();
        //distances = new float[neighborhoods.Length];
        barHappyUI.ChangeBar(happiness);
        happinessROC = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(WaveCode.self.WaveStart)
        {
            happiness += happinessROC;
            barHappyUI.ChangeBar(happiness);
            tempHappinessText.text = "happiness = " + happiness + " happiness rate of change = " + happinessROC;
        } 
    }

    public void CalculateHappiness(float amount) {
        happinessROC += amount;
    }


    /*
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
}
