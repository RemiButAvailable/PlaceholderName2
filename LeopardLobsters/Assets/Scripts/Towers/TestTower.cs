using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TestTower : MonoBehaviour
{
    //public TestDetector attackZone;

    //public PriorityQueue<GameObject> queue;

    //public float cooldown;
    //public bool enemyInZone = false;

    //TestProjectile arrowScript;
    //public int predictedSpot;
    //KnightScript knightScript;
    //KnightScript target => queue[0];
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    queue = new List<GameObject>();
    //    attackZone.enemyEnter.AddListener(EnemyEnter);
    //    attackZone.enemyExit.AddListener(EnemyExit);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (queue.Count > 0)
    //    {
    //        enemyInZone = true;

    //        if (target.health <= 0)
    //        {
    //            EnemyExit(target);
    //        }
    //    }
    //    else
    //    {
    //        enemyInZone = false;
    //    }
    //}
    //public IEnumerator ShootArrows()
    //{
    //    while (enemyInZone)
    //    {
            
    //        TestProjectile arrow = Instantiate(arrowScript, transform.position, Quaternion.identity);
    //        knightScript = queue[0].GetComponent<KnightScript>();
    //        //arrow lerp stuff
            
    //        yield return new WaitForSeconds(cooldown);
    //    }
    //}

    //void EnemyEnter(KnightScript knight) {
    //    if (queue.Count == 0) enemyInZone = true;
    //    queue.Add(knight);
        
    //}
    //void EnemyExit(KnightScript knight)
    //{
    //    queue.Remove(knight);
    //    if (queue.Count == 0) enemyInZone = false;
    //}
}
