using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework.Constraints;

public class ArcherTowerScript : MonoBehaviour
{
    public EnemyDetection attackZone;
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
    public WaveCode waveCode;
    public bool towerManned;
    //(Made by Dante Jones)
    //Sound when enemy dies
    public AudioSource enemyDeathSound;
    //Sound that plays when enemy shoots
    public AudioSource arrowShootSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackZone.Tower = this.gameObject;
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

            if (targetedEnemy.GetComponent<KnightScript>().health <= 0)
            {
                queue.Remove(targetedEnemy);

                //Sound that plasy when enemy dies
                enemyDeathSound.Play();
            }
        }
        else
        {
            enemyInZone = false;
        }
        if(gameObject.GetComponent<BaseTower>().people >= gameObject.GetComponent<BaseTower>().peopleNeeded)
        {
            towerManned = true;
        }
        else
        {
            towerManned = false;
        }
    }
    public IEnumerator ShootArrows()
    {
        while (true)
        {
            if (enemyInZone /*&& towerManned && waveCode.WaveStart*/)
            {
                spawnedArrow = Instantiate(Arrow, transform.position, Quaternion.identity);
                Debug.Log("shooting");

                //Sound when arrow shoots
                arrowShootSound.Play();

                arrowScript = spawnedArrow.GetComponent<ArrowScript>();
                knightScript = queue[0].GetComponent<KnightScript>();

                //predicted spot will be based on enemy speed if we have multiple types of enemies
                Vector3 target = knightScript.waypoints[knightScript.index + predictedSpot];
                arrowScript.direction = target - transform.position;
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
