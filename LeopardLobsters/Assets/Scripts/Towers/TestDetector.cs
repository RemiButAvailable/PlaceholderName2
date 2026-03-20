using UnityEngine;
using UnityEngine.Events;

public class TestDetector : MonoBehaviour
{
    public UnityEvent<KnightScript> enemyEnter;
    public UnityEvent<KnightScript> enemyExit;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            enemyEnter.Invoke(other.GetComponent<KnightScript>());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            enemyEnter.Invoke(other.GetComponent<KnightScript>());
        }
    }
}

