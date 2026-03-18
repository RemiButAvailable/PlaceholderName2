using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ArcherTowerScript : MonoBehaviour
{
    public GameObject AttackZone;
    public List<GameObject> queue;
    public float cooldown;
    public GameObject Arrow;
    public bool enemyInZone = false;
    Vector3 direction;
    ArrowScript arrowScript;
    GameObject spawnedArrow;
    public int predictedSpot;
    KnightScript knightScript;
    GameObject targetedEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        queue = new List<GameObject>();
        StartCoroutine(ShootArrows());
        StartCoroutine(Printer());
    }

    // Update is called once per frame
    void Update()
    {
        if (queue.Count > 0)
        {
            enemyInZone = true;
            targetedEnemy = queue[0];
        }
        else
        {
            enemyInZone = false;
        }

        if(targetedEnemy.GetComponent<KnightScript>().health <= 0)
        {
            queue.Remove(targetedEnemy);
        }
    }
    public IEnumerator ShootArrows()
    {
        while (true)
        {
            Debug.Log(enemyInZone);
            if (enemyInZone)
            {
                spawnedArrow = Instantiate(Arrow, transform.position, Quaternion.identity);
                arrowScript = spawnedArrow.GetComponent<ArrowScript>();
                knightScript = queue[0].GetComponent<KnightScript>();
                //predicted spot will be based on enemy speed if we have multiple types of enemies
                Vector3 target = knightScript.waypoints[knightScript.index + predictedSpot];
                arrowScript.direction = target - transform.position; //placeholder direction
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
    public IEnumerator Printer()
    {
        while(true)
        {
            //Debug.Log("val is " + queue.Count);
            yield return new WaitForSeconds(1);
        }
    }
}
