using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            archerTowerScript.queue.Add(other.gameObject);
            other.gameObject.GetComponent<KnightScript>().inhabitedTowerZone = archerTowerScript;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            archerTowerScript.queue.Remove(other.gameObject);
            other.gameObject.GetComponent<KnightScript>().inhabitedTowerZone = null;
            //GetComponent<SpriteRenderer>().color = Color.red;q
        }
    }
}
