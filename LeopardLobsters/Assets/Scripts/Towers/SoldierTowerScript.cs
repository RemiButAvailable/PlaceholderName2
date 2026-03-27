using UnityEngine;
using System.Collections.Generic;

public class SoldierTowerScript : MonoBehaviour
{
    bool allSoldiersDead;
    public GameObject soldier;
    List<GameObject> soldiers;
    List<Vector3> soldierPositions;
    List<GameObject> enemiesInZone;
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
    public void RemoveSoldier(soldier)
    {
        for (int i = 0; i <= soldierPositions.Count; i++)
        {
            if(Vector3.Distance(soldier))
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemiesInZone.Add(collision.gameObject);
        }
    }
}
