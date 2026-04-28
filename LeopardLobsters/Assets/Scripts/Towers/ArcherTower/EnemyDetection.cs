using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{

    //public GameObject Tower;
    //ArcherTowerScript archerTowerScript;
    public UnityEvent<KnightScript> KnightEntered;
    public UnityEvent<KnightScript> KnightExited;

    void Start()
    {
        //archerTowerScript = Tower.GetComponent<ArcherTowerScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            KnightEntered.Invoke(other.gameObject.GetComponent<KnightScript>());
            

            //other.gameObject.GetComponent<KnightScript>().inhabitedTowerZone = archerTowerScript;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            KnightExited.Invoke(other.gameObject.GetComponent<KnightScript>());

            //what about multiple towers
            //other.gameObject.GetComponent<KnightScript>().inhabitedTowerZone = null;
        }
    }
}
