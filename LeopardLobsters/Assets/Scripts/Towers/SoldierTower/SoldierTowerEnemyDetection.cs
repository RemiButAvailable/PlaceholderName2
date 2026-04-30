
using UnityEngine;

public class SoldierTowerEnemyDetection : MonoBehaviour
{
    public GameObject soldierTower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "knight")
        {
            soldierTower.GetComponent<SoldierTowerScript>().AddEnemy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "knight")
        {
            soldierTower.GetComponent<SoldierTowerScript>().RemoveEnemy(collision.gameObject);
        }
    }
}
