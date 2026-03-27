/*
 * Remi de Plater
 * 3/27/26
 * Soldier Tower Functionality
 */
using UnityEngine;
using System.Collections.Generic;

public class SoldierTowerScript : MonoBehaviour
{
    bool allSoldiersDead;
    public GameObject soldier;
    public List<GameObject> soldiers;
    List<Vector3> soldierPositions;
    List<GameObject> enemiesInZone;
    //GameObject spa
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddSoldier(GameObject soldier)
    {
        
        soldiers.Add(soldier);
        for(int i = 0; i <= soldierPositions.Count; i++)
        {
            if (soldierPositions[i].z == 0)
            {
                soldier.transform.position = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 1);
                soldier.GetComponent<SoldierScript>().stationPosition = soldier.transform.position;
                break;
            }
        }
    }
    public void RemoveSoldier(GameObject soldier)
    {
        for (int i = 0; i <= soldierPositions.Count; i++)
        {
            if(Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, soldierPositions[i]) < 0.1f)
            {
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                if(soldier.GetComponent<SoldierScript>().target != null)
                {
                    //
                }
                soldiers.Remove(soldier);
                //play death sound?
                break;
            }
        }
    }
    public void AddEnemy(GameObject enemy)
    {
        enemiesInZone.Add(enemy);
        foreach (var soldier in soldiers)
        {
            if (soldier.GetComponent<SoldierScript>().engaged == false)
            {
                soldier.GetComponent<SoldierScript>().target = enemy;
                soldier.GetComponent<SoldierScript>().engaged = true;
                break;
            }
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
        foreach (var soldier in soldiers)
        {
            if (soldier.GetComponent<SoldierScript>().target.GetInstanceID() == enemy.GetInstanceID())
            {
                soldier.GetComponent<SoldierScript>().target = null;
                soldier.GetComponent<SoldierScript>().engaged = false;
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemiesInZone.Add(collision.gameObject);
            foreach(var soldier in soldiers)
            {
                if(soldier.GetComponent<SoldierScript>().engaged == false)
                {
                    soldier.GetComponent<SoldierScript>().target = collision.gameObject;
                    soldier.GetComponent<SoldierScript>().engaged = true;
                    break;
                }
            }
        }
    }*/
    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemiesInZone.Remove(collision.gameObject);
            foreach(var soldier in soldiers)
            {
                if(soldier.GetComponent<SoldierScript>().target.GetInstanceID() == collision.gameObject.GetInstanceID())
                {
                    soldier.GetComponent<SoldierScript>().target = null;
                    soldier.GetComponent<SoldierScript>().engaged = false;
                }
            }
        }
    }*/
}
