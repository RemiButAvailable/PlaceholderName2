using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework.Constraints;

public class ArcherTowerScript : MonoBehaviour
{
    public EnemyDetection attackZone;
    public List<KnightScript> queue;
    public bool enemyInZone => queue.Count>0;

    [SerializeField] Transform arrowStartPosition;
    [SerializeField] float cooldown;
    [SerializeField] int predictedSpot;
    [SerializeField] ArrowScript Arrow;

    public bool isActive;

    //(Made by Dante Jones)
    //Sound that plays when enemy shoots
    [SerializeField] AudioSource arrowShootSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //attackZone.Tower = this.gameObject;
        attackZone.KnightEntered.AddListener(EnemyEntered);
        attackZone.KnightExited.AddListener(EnemyExited);

        queue = new List<KnightScript>();
        StartCoroutine(ShootArrows());
        //StartCoroutine(Printer());
        GetComponent<BaseTower>().isActive.AddListener(IsActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyInZone)
        {
            GameObject targetedEnemy = queue[0];

            if (targetedEnemy.GetComponent<KnightScript>().health <= 0)
            {
                queue.Remove(targetedEnemy);
            }
        }
    }

    public void EnemyEntered(KnightScript enemy) {
        if (queue.Contains(enemy)) return;
        queue.Add(enemy);
        printQueue();
    }
    public void EnemyExited(KnightScript enemy) {
        queue.Remove(enemy);
        printQueue();
    }


    public IEnumerator ShootArrows()
    {
        while (true)
        {
            if (enemyInZone && isActive)
            {
                ArrowScript arrowScript = Instantiate(Arrow, transform.position, Quaternion.identity);

                //Sound when arrow shoots
                arrowShootSound.Play();

                queue.Sort();
                //KnightScript knightScript = queue[0].GetComponent<KnightScript>();
                KnightScript knightScript = queue[0];

                //predicted spot will be based on enemy speed if we have multiple types of enemies
                Vector3 target = knightScript.waypoints[knightScript.index + predictedSpot/* * (int)(knightScript.speed * directionMultiplier)*/] + knightScript.offset;
                arrowScript.target = target;
                arrowScript.start = arrowStartPosition.transform.position;



                /*float knightDistToNextTurn = Vector3.Distance(queue[0].transform.position, knightScript.nextWayPoint);
                float minimumDistance;
                if(knightDistToNextTurn < minimumDistance)
                {

                }*/
                //arrowScript.direction = queue[0].transform.position + (knightScript.direction.normalized) * (knightScript.speed * multiplier) - transform.position;

                //arrowPosition(t) = knightPosition(t)
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    void IsActive(bool active) { isActive = active; }

    //public void ChangeTarget(GameObject knight)
    //{
    //    queue[0] = knight;
    //}

    void printQueue() {
        string full = "";
        foreach (KnightScript knight in queue) {
            full += knight + ", ";
        }
        print(full);
    }
}
