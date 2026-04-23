/*
 * Remi de Plater
 * 3/27/26
 * Soldier Tower Functionality
 */
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class SoldierTowerScript : MonoBehaviour
{
    bool allSoldiersDead;
    public GameObject soldier;
    public List<GameObject> soldiers;
    List<Vector3> soldierPositions;
    public List<GameObject> enemiesInZone;
    [SerializeField]BaseTower baseTower;
    public int soldierSpawnPosDistFromClosestPointOnPath;
    GameObject castleObj;
    PolygonCollider2D radius;

    [SerializeField] AudioSource RemoveSoldierSound;
    [SerializeField] AudioSource SoldierDeathSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseTower.AddedPeople.AddListener(AddSoldier);
        baseTower.RemovedPeople.AddListener(RemoveSoldierViaButton);
        baseTower.OnPlace.AddListener(SetSoldierStationPositions);
    }

    public void AddSoldier()
    {
        GameObject spawnedSoldier = Instantiate(soldier, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedSoldier.GetComponent<SoldierScript>().soldierTower = this.gameObject;
        soldiers.Add(spawnedSoldier);
        for(int i = 0; i < soldierPositions.Count; i++)
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
    public void RemoveSoldierViaButton()
    {
        GameObject soldier = soldiers[0];
        for (int i = 0; i < soldierPositions.Count; i++)
        {
            Vector3 convertedSoldierPosition = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
            if(Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, convertedSoldierPosition) < 0.1f)
            {
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);

                if(soldier.GetComponent<SoldierScript>().target != null)
                {
                    soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().speed = soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().defaultSpeed;
                    soldier.GetComponent<SoldierScript>().target.GetComponent<KnightScript>().targeted = false;
                }
                soldiers.Remove(soldier);
                Destroy(soldier);

                break;
            }
        }
    }
    public void RemoveSoldier(GameObject soldier)
    {
        for (int i = 0; i < soldierPositions.Count; i++) //set the soldier's station position to empty
        {
            Vector3 convertedSoldierPosition = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
            if (Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, convertedSoldierPosition) < 0.1f)
            {
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                soldiers.Remove(soldier);
                GetComponent<BaseTower>().people -= 1;
                SoldierDeathSound?.Play();
                break;
            }
        }
        for(int i = 0; i < soldiers.Count; i++) //check if there's any soldiers who don't have a target and direct them to attack the enemy that's closest to reaching the castle
        {
            if (soldiers[i].GetComponent<SoldierScript>().target == null)
            {
                enemiesInZone.Sort();
                for(int o = 0; o < enemiesInZone.Count; o++)
                {
                    bool canReachEnemy = false;

                    //find the point at which the enemy will leave the radius
                    Vector3 radiusPathIntersectionPoint = new Vector3(0, 0, 0);
                    //foreach(Vector3 pointAlongEdgeOfRadius in radius.points)
                    //
                    LineRenderer enemyO_LR = enemiesInZone[o].GetComponent<KnightScript>().lineRenderer;
                    enemyO_LR.useWorldSpace = false;
                    EdgeCollider2D enemyPathColldier2D = enemiesInZone[o].AddComponent<EdgeCollider2D>();
                    Vector3[] pointsAlongEnemyPath = new Vector3[enemyO_LR.positionCount];
                    enemyO_LR.GetPositions(pointsAlongEnemyPath);
                    Vector2[] convertedArr = System.Array.ConvertAll(pointsAlongEnemyPath, v => new Vector2(v.x, v.y));
                    enemyPathColldier2D.points = convertedArr;
                    enemyPathColldier2D.edgeRadius = 0.01f;

                    EdgeCollider2D radiusCollider2D = gameObject.AddComponent<EdgeCollider2D>();
                    radiusCollider2D.points = radius.points;
                    radiusCollider2D.edgeRadius = 0.01f;

                    int layerMask = 1 << LayerMask.NameToLayer("edgeColliders");
                    ContactFilter2D filter = new ContactFilter2D();
                    filter.SetLayerMask(layerMask);
                    ContactPoint2D[] contacts = new ContactPoint2D[12];
                    int contactCount = radiusCollider2D.GetContacts(filter, contacts);

                    Vector2 closestContactToCastle = contacts[0].point;
                    for(int p = 0; p < contactCount; p++)
                    {
                        if (Vector3.Distance(contacts[i].point, castleObj.transform.position) < Vector3.Distance(closestContactToCastle, castleObj.transform.position))
                        {
                            closestContactToCastle = contacts[i].point;
                        }
                    }
                    //}

                    //check if the enemy will leave the radius before the knight reaches them
                    float timeForSoldierToReachKnight = 0;
                    float timeForKnightToReachRadiusEdge = Vector2.Distance(enemiesInZone[o].transform.position, closestContactToCastle) / enemiesInZone[o].GetComponent<KnightScript>().speed;
                    Vector2 enemyDir = closestContactToCastle - (Vector2)enemiesInZone[o].transform.position;
                    for(float t = 0; t < 1; t++)
                    {
                        //check
                    }

                    if (Vector3.Distance(enemiesInZone[o].transform.position, radius.ClosestPoint(enemiesInZone[o].transform.position)) >= Vector3.Distance(enemiesInZone[o].transform.position, soldiers[i].transform.position))
                    {
                        canReachEnemy = true;
                    }
                    if(enemiesInZone[o].GetComponent<KnightScript>().targeted == false && canReachEnemy)
                    {
                        soldiers[i].GetComponent<SoldierScript>().target = enemiesInZone[o];
                    }
                }
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
                enemy.GetComponent<KnightScript>().targeted = true;
                soldier.GetComponent<SoldierScript>().engaged = true;
                Debug.Log(soldier.GetComponent<SoldierScript>().engaged);
                break;
            }
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
        foreach (var soldier in soldiers)
        {
            if(soldier.GetComponent<SoldierScript>().target != null)
            if (soldier.GetComponent<SoldierScript>().target.GetInstanceID() == enemy.GetInstanceID())
            {
                soldier.GetComponent<SoldierScript>().target = null;
                soldier.GetComponent<SoldierScript>().engaged = false;
            }
        }
    }

    [System.Obsolete]
    public void SetSoldierStationPositions(BaseTower tower)
    {
        //Arrays
        LineRenderer[] enemyPaths = Object.FindObjectsOfType<LineRenderer>(); //array of the line renderers
        float[] closestPointsOnEachLineDists = new float[enemyPaths.Length]; //array of distances from the closest point to the tower on each line to the tower

        for(int i = 0; i < closestPointsOnEachLineDists.Length; i++)//set each dist to a default val
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
            for(int o = 0; o < enemyPaths[i].positionCount; o++)
            {
                Vector3 convertedPosO = new Vector3(enemyPaths[i].GetPosition(o).x, enemyPaths[i].GetPosition(o).y, 0);//set z to 0
                if(Vector3.Distance(convertedPosO, transform.position) < closestPointsOnEachLineDists[i])//if current dist is less than prev, set closest vars to current one
                {
                    closestPointsOnEachLineDists[i] = Vector3.Distance(convertedPosO, transform.position);
                    closestPointsOnEachLine[i] = convertedPosO;
                    closestPointsOnEachLineIndex[i] = o;
                }
            }
        }
        for(int i = 0; i < closestPointsOnEachLineDists.Length; i++) //check which point is closest to the tower of the three selected
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

    public bool CheckIfSoldierCanReachEnemy(int o_index)
    {
        bool canReachEnemy = false;

        //find the point at which the enemy will leave the radius

        //make an edge collider from the points along the enemy path
        LineRenderer enemyO_LR = enemiesInZone[o_index].GetComponent<KnightScript>().lineRenderer;
        enemyO_LR.useWorldSpace = false;
        EdgeCollider2D enemyPathColldier2D = enemiesInZone[o_index].AddComponent<EdgeCollider2D>();
        Vector3[] pointsAlongEnemyPath = new Vector3[enemyO_LR.positionCount];
        enemyO_LR.GetPositions(pointsAlongEnemyPath);
        Vector2[] convertedArr = System.Array.ConvertAll(pointsAlongEnemyPath, v => new Vector2(v.x, v.y));
        enemyPathColldier2D.points = convertedArr;
        enemyPathColldier2D.edgeRadius = 0.01f;

        //make an edge collider from the points along the circumfrence of the radius
        EdgeCollider2D radiusCollider2D = gameObject.AddComponent<EdgeCollider2D>();
        radiusCollider2D.points = radius.points;
        radiusCollider2D.edgeRadius = 0.01f;

        //find all contact points between the two
        int layerMask = 1 << LayerMask.NameToLayer("edgeColliders");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layerMask);
        ContactPoint2D[] contacts = new ContactPoint2D[12];
        int contactCount = radiusCollider2D.GetContacts(filter, contacts);

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
        float timeForSoldierToReachKnight = 0;
        float timeForKnightToReachRadiusEdge = Vector2.Distance(enemiesInZone[o_index].transform.position, closestContactToCastle) / enemiesInZone[o_index].GetComponent<KnightScript>().speed;
        Vector2 enemyDir = closestContactToCastle - (Vector2)enemiesInZone[o_index].transform.position;
        for (float t = 0; t < 12; t++)
        {
            
        }
        return canReachEnemy;
    }
}
