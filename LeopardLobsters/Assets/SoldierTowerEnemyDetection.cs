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
        if (collision.gameObject.tag == "enemy")
        {
            /*enemiesInZone.Add(collision.gameObject);
            foreach (var soldier in soldiers)
            {
                if (soldier.GetComponent<SoldierScript>().engaged == false)
                {
                    soldier.GetComponent<SoldierScript>().target = collision.gameObject;
                    soldier.GetComponent<SoldierScript>().engaged = true;
                    break;
                }
            }*/
            soliderTower.GetComponent<SoldierTowerScript>().AddEnemy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            /*enemiesInZone.Remove(collision.gameObject);
            foreach (var soldier in soldiers)
            {
                if (soldier.GetComponent<SoldierScript>().target.GetInstanceID() == collision.gameObject.GetInstanceID())
                {
                    soldier.GetComponent<SoldierScript>().target = null;
                    soldier.GetComponent<SoldierScript>().engaged = false;
                }
            }*/
            soliderTower.GetComponent<SoldierTowerScript>().RemoveEnemy(collision.gameObject);
        }
    }
}
