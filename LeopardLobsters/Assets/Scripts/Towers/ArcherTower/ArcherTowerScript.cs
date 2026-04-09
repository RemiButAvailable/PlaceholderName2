using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework.Constraints;

public class ArcherTowerScript : MonoBehaviour
{
    public EnemyDetection attackZone;
    public List<GameObject> queue;
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
        attackZone.Tower = this.gameObject;
        queue = new List<GameObject>();
        StartCoroutine(ShootArrows());
        //StartCoroutine(Printer());
        GetComponent<BaseTower>().isActive.AddListener(IsActive);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (enemyInZone)
        {
            if (queue[0].GetComponent<KnightScript>().health <= 0)
            {
                queue.Remove(queue[0]);
            }
        }*/
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

                //KnightScript knightScript = queue[0].GetComponent<KnightScript>();
                KnightScript knightScript = queue[0].GetComponent<KnightScript>();

                //predicted spot will be based on enemy speed if we have multiple types of enemies
                Vector3 target = knightScript.waypoints[knightScript.index + predictedSpot/* * (int)(knightScript.speed * directionMultiplier)*/] + knightScript.offset;
                arrowScript.target = target;
                arrowScript.start = arrowStartPosition.transform.position;
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    void IsActive(bool active) { isActive = active; }

    public void ChangeTarget(GameObject knight)
    {
        queue[0] = knight;
    }
}
