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
        }
        else
        {
            enemyInZone = false;
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
                arrowScript.direction = queue[0].transform.position - transform.position; //placeholder direction
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
