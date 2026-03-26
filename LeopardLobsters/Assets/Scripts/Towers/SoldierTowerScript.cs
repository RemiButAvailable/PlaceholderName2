using UnityEngine;

public class SoldierTowerScript : MonoBehaviour
{
    bool allSoldiersDead;
    //public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public IEnumerator ShootArrows()
    {
        while (true)
        {
            if (allSoldiersDead)
            {
                spawnedArrow = Instantiate(Arrow, transform.position, Quaternion.identity);
                arrowScript = spawnedArrow.GetComponent<ArrowScript>();
                knightScript = queue[0].GetComponent<KnightScript>();

                //predicted spot will be based on enemy speed if we have multiple types of enemies
                Vector3 target = knightScript.waypoints[knightScript.index + predictedSpot];
                arrowScript.direction = target - transform.position;
            }
            yield return new WaitForSeconds(cooldown);
        }
    }*/
}
