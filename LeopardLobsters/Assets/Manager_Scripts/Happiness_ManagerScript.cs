using UnityEngine;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Collider2D[] neighborhoods;
    public float[] distances;
    float impactOnHappiness;
    public float happiness;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distances = new float[neighborhoods.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CalculateHappiness(GameObject building)
    {
        for (int i = 0; i < neighborhoods.Length; i++)
        {
            distances[i] = Vector3.Distance(neighborhoods[i].ClosestPoint(building.transform.position), building.transform.position);
            if (building.tag == "tower")
            {
                impactOnHappiness -= distances[i];
            }
            else
            {
                impactOnHappiness += distances[i];
            }
            Debug.Log("impact on happiness = " + impactOnHappiness);
        }
        happiness += impactOnHappiness;
    }
}
