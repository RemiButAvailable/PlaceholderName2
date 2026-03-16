using UnityEngine;

public class DetectEnemiesScript : MonoBehaviour
{
    public GameObject Tower;
    ArcherTowerScript archerTowerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        archerTowerScript = Tower.GetComponent<ArcherTowerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(PolygonCollider2D other)
    {
        if(other.gameObject.tag == "knight")
        {
            archerTowerScript.queue.Add(other.gameObject);
        }
    }
    void OnTriggerExit(PolygonCollider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            archerTowerScript.queue.Remove(other.gameObject);
        }
    }
        
}
