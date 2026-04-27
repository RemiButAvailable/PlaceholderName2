using UnityEngine;

public class SoldierTowerEnemyDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject soliderTower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "knight")
        {
            soliderTower.GetComponent<SoldierTowerScript>().AddEnemy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "knight")
        {
            soliderTower.GetComponent<SoldierTowerScript>().RemoveEnemy(collision.gameObject);
        }
    }
}
