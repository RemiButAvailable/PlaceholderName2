/*
 * SoldierTowerScript.cs
 * Remi de Plater
 * remi.deplater@digipen.edu
 * Soldier Tower Functionality
 */
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using System;

public class SoldierTowerScript : MonoBehaviour
{
    //bools
    bool allSoldiersDead;
    bool ranCoroutine;
    bool canReachEnemy = false;

    //GameOb

    //GameObjects


    public GameObject soldier;
    public List<GameObject> soldiers;
    List<Vector3> soldierPositions;
    public List<GameObject> enemiesInZone;
    [SerializeField]BaseTower baseTower;
    public int soldierSpawnPosDistFromClosestPointOnPath;
    GameObject castleObj;
    public PolygonCollider2D radius;

    [SerializeField] AudioSource RemoveSoldierSound;
    [SerializeField] AudioSource SoldierDeathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseTower.AddedPeople.AddListener(AddSoldier);
        baseTower.RemovedPeople.AddListener(RemoveSoldierViaButton);
        baseTower.OnPlace.AddListener(SetSoldierStationPositions);
    }

    /*
     * Name: AddSoldier
     * 
     * Desc: logic for adding a soldier
     */
    public void AddSoldier()
    {
        GameObject spawnedSoldier = Instantiate(soldier, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedSoldier.GetComponent<SoldierScript>().soldierTower = this.gameObject;
        soldiers.Add(spawnedSoldier);

        for(int i = 0; i < soldierPositions.Count; i++)//find an open station, set the soldier's position to it, and set it to filled
        {
            if (soldierPositions[i].z == 0)
            {
                spawnedSoldier.transform.position = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 1);
                spawnedSoldier.GetComponent<SoldierScript>().stationPosition = spawnedSoldier.transform.position;
                break;
            }
        }
    }

    /*
     * Name: RemoveSoldierViaButton
     * 
     * Input: soldier - the soldier that has to be removed
     * 
     * Desc: logic for removing a soldier (button version)
     */
    public void RemoveSoldierViaButton()
    {
        GameObject soldier = soldiers[0];//default to first soldier in the array if removing with button
        soldiers.Remove(soldier);

        if (soldiers.Count > 0)
        {
            for (int i = 0; i < soldierPositions.Count; i++)//set the soldier's station position to empty
            {
                Vector3 convertedSoldierPosition = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);//convert soldier positions i to be able to be compared to the removed soldier's station position
                if (Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, convertedSoldierPosition) < 0.1f)//if converted soldier position is the removed soldier's position
                {
                    soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);

                    if (soldier.GetComponent<SoldierScript>().target != null)
                    {
                        soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().speed = soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().defaultSpeed;
                        soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().targeted = false;
                    }
                    Destroy(soldier);
                    break;
                }
            }

            for (int i = 0; i < soldiers.Count; i++) //check if there's any soldiers who don't have a target and direct them to attack the enemy that's closest to reaching the castle
            {
                if (soldiers[i].GetComponent<SoldierScript>().target == null)
                {
                    enemiesInZone.Sort();
                    for (int o = 0; o < enemiesInZone.Count; o++)
                    {
                        /*EdgeCollider2D radiusCollider2D = CheckIfSoldierCanReachEnemy(i, o);
                        ranCoroutine = false;
                        StartCoroutine(CheckForContacts(radiusCollider2D, i, o));*/
                        if (enemiesInZone[o].GetComponent<KnightScript>().targeted == false/* && canReachEnemy*/)
                        {
                            soldiers[i].GetComponent<SoldierScript>().target = enemiesInZone[o];
                        }
                    }
                }
            }
        }
        canReachEnemy = false;
    }

    /*
     * Name: RemoveSoldier
     * 
     * Input: soldier - the soldier that has to be removed
     * 
     * Desc: logic for removing a soldier (death version)
     */
    [System.Obsolete]
    public void RemoveSoldier(GameObject soldier)
    {
        Castle.self.PersonDead(soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>());
        soldiers.Remove(soldier);

        if (soldiers.Count > 0)
        {
            for (int i = 0; i < soldierPositions.Count; i++)//set the soldier's station position to empty
            {
                Vector3 convertedSoldierPosition = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);//convert soldier positions i to be able to be compared to the removed soldier's station position
                if (Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, convertedSoldierPosition) < 0.1f)//if converted soldier position is the removed soldier's position
                {
                    soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                    GetComponent<BaseTower>().people -= 1;
                    break;
                }
            }

            for (int i = 0; i < soldiers.Count; i++) //check if there's any soldiers who don't have a target and direct them to attack the enemy that's closest to reaching the castle
            {
                if (soldiers[i].GetComponent<SoldierScript>().target == null)
                {
                    enemiesInZone.Sort();
                    for (int o = 0; o < enemiesInZone.Count; o++)
                    {
                        /*EdgeCollider2D radiusCollider2D = CheckIfSoldierCanReachEnemy(i, o);
                        ranCoroutine = false;
                        StartCoroutine(CheckForContacts(radiusCollider2D, i, o));*/
                        if (enemiesInZone[o].GetComponent<KnightScript>().targeted == false/* && canReachEnemy*/)
                        {
                            soldiers[i].GetComponent<SoldierScript>().target = enemiesInZone[o];
                        }
                    }
                }
            }
        }
        SoldierDeathSound?.Play();
    }

    //Logic for adding an enemy to the zone
    public void AddEnemy(GameObject enemy)
    {
        enemiesInZone.Add(enemy);
        foreach (var soldier in soldiers)
        {
            if (soldier.GetComponent<SoldierScript>().engaged == false)
            {
                soldier.GetComponent<SoldierScript>().target = enemy;
                enemy.GetComponent<KnightScript>().targeted = true;
                soldier.GetComponent<SoldierScript>().engaged = true;
                break;
            }
        }
    }

    //Logic for removing an enemy from the zone
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
        foreach (var soldier in soldiers)
        {
            if (soldier.GetComponent<SoldierScript>().target != null)
                if (soldier.GetComponent<SoldierScript>().target.GetInstanceID() == enemy.GetInstanceID())
                {
                    soldier.GetComponent<SoldierScript>().target = null;
                    soldier.GetComponent<SoldierScript>().engaged = false;
                }
        }
    }

    /*
     * Name: SetSoldierStationPositions
     * 
     * Input: tower - refrence to the BaseTower script attached to the obj
     * 
     * Desc: sets the soldier station positions to be the closest points along the closest enemy path
     */
    [System.Obsolete]
    public void SetSoldierStationPositions(BaseTower tower)
    {
        //Arrays
        LineRenderer[] enemyPaths = UnityEngine.Object.FindObjectsOfType<LineRenderer>(); //array of the line renderers
        float[] closestPointsOnEachLineDists = new float[enemyPaths.Length]; //array of distances from the closest point to the tower on each line to the tower

        for (int i = 0; i < closestPointsOnEachLineDists.Length; i++)//set each dist to a default val
        {
            closestPointsOnEachLineDists[i] = 100;
        }

        int[] closestPointsOnEachLineIndex = new int[enemyPaths.Length]; //array of the indexes of these points in each line renderer's array
        Vector3[] closestPointsOnEachLine = new Vector3[enemyPaths.Length]; //array of the points themselves
        float closestPoint = 100; //point for comparing against
        LineRenderer closestLine; //the closest line renderer to the tower
        int selectedIndex = 0; //the index of the closest line renderer to the tower

        //Checking loops
        for (int i = 0; i < enemyPaths.Length; i++) //for each line renderer, check which point along it is closes to the tower
        {
            for (int o = 0; o < enemyPaths[i].positionCount; o++)
            {
                Vector3 convertedPosO = new Vector3(enemyPaths[i].GetPosition(o).x, enemyPaths[i].GetPosition(o).y, 0);//set z to 0
                if (Vector3.Distance(convertedPosO, transform.position) < closestPointsOnEachLineDists[i])//if current dist is less than prev, set closest vars to current one
                {
                    closestPointsOnEachLineDists[i] = Vector3.Distance(convertedPosO, transform.position);
                    closestPointsOnEachLine[i] = convertedPosO;
                    closestPointsOnEachLineIndex[i] = o;
                }
            }
        }
        for (int i = 0; i < closestPointsOnEachLineDists.Length; i++) //check which point is closest to the tower of the three selected
        {
            if (closestPointsOnEachLineDists[i] < closestPoint)
            {
                closestPoint = closestPointsOnEachLineDists[i];
                selectedIndex = i;
            }
        }
        closestLine = enemyPaths[selectedIndex];

        //converted points for soldiers on the left and right of the middle one
        Vector3 convertedPoint1 = new Vector3(enemyPaths[selectedIndex].GetPosition(closestPointsOnEachLineIndex[selectedIndex] + soldierSpawnPosDistFromClosestPointOnPath).x, enemyPaths[selectedIndex].GetPosition(closestPointsOnEachLineIndex[selectedIndex] + soldierSpawnPosDistFromClosestPointOnPath).y, 0);
        Vector3 convertedPoint2 = new Vector3(enemyPaths[selectedIndex].GetPosition(closestPointsOnEachLineIndex[selectedIndex] - soldierSpawnPosDistFromClosestPointOnPath).x, enemyPaths[selectedIndex].GetPosition(closestPointsOnEachLineIndex[selectedIndex] - soldierSpawnPosDistFromClosestPointOnPath).y, 0);
        soldierPositions = new List<Vector3>() //set the station positions to the closest spot and two spots ahead or behind in the line renderer's index
        {
            closestPointsOnEachLine[selectedIndex],
            convertedPoint1,
            convertedPoint2
        };
    }


    /*
     * Name: CheckIfSoldierCanReachEnemy
     * 
     * Input: i_index - which soldier is being parsed in
     *        o_index - which enemy is being parsed in
     *        
     * Output: radiusCollider2D - edge collider with the same shape and size as the tower's radius
     * 
     * Desc: generates an edge collider equivalent of the radius and the enemy path
     */
    [System.Obsolete]
    public EdgeCollider2D CheckIfSoldierCanReachEnemy(int i_index, int o_index)
    {
        //find the point at which the enemy will leave the radius

        //Get the enemy's path
        LineRenderer enemyO_LR = enemiesInZone[o_index].GetComponent<KnightScript>().lineRenderer;

        //make an edge collider from the points along the enemy path
        EdgeCollider2D enemyPathCollider2D = enemyO_LR.gameObject.AddComponent<EdgeCollider2D>();

        //parse the line renderer's points into an array that'll be converted to be the right dimensions
        Vector3[] child_points = new Vector3[enemyO_LR.positionCount];
        enemyO_LR.GetPositions(child_points);

        Vector2[] convertedArr = System.Array.ConvertAll(child_points, v => new Vector2(v.x, v.y));
        enemyPathCollider2D.points = convertedArr;
        enemyPathCollider2D.edgeRadius = 0.1f;

        enemyO_LR.gameObject.layer = LayerMask.NameToLayer("edgeColliders");

        //make an edge collider from the points along the circumfrence of the radius
        EdgeCollider2D radiusCollider2D = gameObject.AddComponent<EdgeCollider2D>();
        radiusCollider2D.isTrigger = false;

        //parse the radius's points into an array that'll be converted to be the right dimensions
        Vector2[] childPoints = radius.points;
        Vector2[] parentPoints = new Vector2[childPoints.Length];
        
        for(int i = 0; i < childPoints.Length; i++)
        {
            Vector2 worldPoint = radius.transform.TransformPoint(childPoints[i]);
            parentPoints[i] = radiusCollider2D.transform.InverseTransformPoint(worldPoint);
        }

        radiusCollider2D.points = parentPoints;
        radiusCollider2D.edgeRadius = 0.1f;

        gameObject.layer = LayerMask.NameToLayer("edgeColliders");

        return radiusCollider2D;
    }

    /*
     * Name: CheckIfSoldierCanReachEnemy
     * 
     * Input: i_index - which soldier is being parsed in
     *        o_index - which enemy is being parsed in
     *        
     * Output: radiusCollider2D - edge collider with the same shape and size as the tower's radius
     * 
     * Desc: generates an edge collider equivalent of the radius and the enemy path
     */
    IEnumerator CheckForContacts(EdgeCollider2D radiusCollider2D, int i_index, int o_index)
    {
        yield return new WaitForFixedUpdate();

        if (ranCoroutine == false)
        {
            Physics2D.SyncTransforms();
            int layerMask = 1 << LayerMask.NameToLayer("edgeColliders");
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(layerMask);
            ContactPoint2D[] contacts = new ContactPoint2D[12];
            int contactCount = radiusCollider2D.GetContacts(/*filter, */contacts);
            Debug.Log("contact count = " + contactCount);

            //find the contact point that's closest to the castle
            Vector2 closestContactToCastle = contacts[0].point;
            for (int p = 0; p < contactCount; p++)
            {
                if (Vector3.Distance(contacts[p].point, castleObj.transform.position) < Vector3.Distance(closestContactToCastle, castleObj.transform.position))
                {
                    closestContactToCastle = contacts[p].point;
                }
            }

            //check if the enemy will leave the radius before the knight reaches them
            Vector2 enemyStartingPos = (Vector2)enemiesInZone[o_index].transform.position;
            Vector2 enemyDir = (closestContactToCastle - (Vector2)enemiesInZone[o_index].transform.position).normalized;
            Vector2 soldierStartingPos = (Vector2)soldiers[i_index].transform.position;

            for (float t = 0; t < 12; t += Time.deltaTime)//checks if the soldier can reach the knight within 12 seconds
            {
                Vector2 enemyPosAtTimeT = enemyStartingPos + enemyDir * enemiesInZone[o_index].GetComponent<KnightScript>().speed * t;
                float soldierDistAtTimeT = Vector2.Distance(soldierStartingPos, enemyPosAtTimeT);
                float distanceSoldierCanTravel = soldiers[i_index].GetComponent<SoldierScript>().speed * t;

                if (distanceSoldierCanTravel >= soldierDistAtTimeT)
                {
                    canReachEnemy = true;
                    ranCoroutine = true;
                    break;
                }
            }
            ranCoroutine = true;
        }
    }
}
