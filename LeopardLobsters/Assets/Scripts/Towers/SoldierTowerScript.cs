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
    BaseTower baseTower;
    public int soldierSpawnPosDistFromClosestPointOnPath;


    [SerializeField] AudioSource RemoveSoldierSound;
    [SerializeField] AudioSource SoldierDeathSound;
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
        GameObject spawnedSoldier = Instantiate(soldier, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedSoldier.GetComponent<SoldierScript>().Tower = this.gameObject;
        soldiers.Add(spawnedSoldier);
        for(int i = 0; i <= soldierPositions.Count; i++)
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
    public void RemoveSoldierViaButton() //connected through inspector
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

                //(Dante Made this)
                RemoveSoldierSound.Play();
                break;
            }
        }
    }
    public void RemoveSoldier(GameObject soldier)
    {
        for (int i = 0; i < soldierPositions.Count; i++)
        {
            Vector3 convertedSoldierPosition = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
            if (Vector3.Distance(soldier.GetComponent<SoldierScript>().stationPosition, convertedSoldierPosition) < 0.1f)
            {
                soldierPositions[i] = new Vector3(soldierPositions[i].x, soldierPositions[i].y, 0);
                soldiers.Remove(soldier);
                Destroy (soldier);
                GetComponent<BaseTower>().people -= 1;
                SoldierDeathSound.Play();
                //play death sound?
                break;
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
    public void SetSoldierStationPositions()
    {
        LineRenderer[] enemyPaths = Object.FindObjectsOfType<LineRenderer>(); //array of the line renderers
        float[] closestPointsOnEachLineDists = new float[enemyPaths.Length]; //array of distances from the closest point to the tower on each line to the tower
        int[] closestPointsOnEachLineIndex = new int[enemyPaths.Length]; //array of the indexes of these points in each line renderer's array
        Vector3[] closestPointsOnEachLine = new Vector3[enemyPaths.Length]; //array of the points themselves
        float closestPoint = 100; //point for comparing against
        LineRenderer closestLine; //the closest line renderer to the tower
        int selectedIndex = 0; //the index of the closest line renderer to the tower
        for (int i = 0; i < enemyPaths.Length; i++) //for each line renderer, check which point along it is closes to the tower
        {
            for(int o = 0; o < enemyPaths[i].positionCount; o++)
            {
                if(Vector3.Distance(enemyPaths[i].GetPosition(o), transform.position) < closestPointsOnEachLineDists[i])
                {
                    closestPointsOnEachLineDists[i] = Vector3.Distance(enemyPaths[i].GetPosition(o), transform.position);
                    closestPointsOnEachLine[i] = enemyPaths[i].GetPosition(o); 
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
        soldierPositions = new List<Vector3>() //set the station positions to the closest spot and two spots ahead or behind in the line renderer's index
        {
            closestPointsOnEachLine[selectedIndex],
            enemyPaths[selectedIndex].GetPosition((closestPointsOnEachLineIndex[selectedIndex]) + soldierSpawnPosDistFromClosestPointOnPath),
            enemyPaths[selectedIndex].GetPosition((closestPointsOnEachLineIndex[selectedIndex]) - soldierSpawnPosDistFromClosestPointOnPath)
            /*new Vector3(transform.position.x - 2, transform.position.y, 0),
            new Vector3(transform.position.x, transform.position.y - 2, 0),
            new Vector3(transform.position.x + 2, transform.position.y, 0)*/
        };
        foreach(var loc in soldierPositions)
        {
            Debug.Log(loc);
        }
    }
}
