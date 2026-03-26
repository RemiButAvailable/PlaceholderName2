/* Author: Victoria T. (And two others)
 * Date: 3/16/26
 * 
 * Description: The code for the wave/rounds of the game,
 * will also keep track of stuff like enemy number, and total wealth.*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

public class WaveCode : MonoBehaviour
{

    // Random unknown objects go WHEEEEE
    public int EnemyNum = 0;
    private int PhantomEnemyNum = 0;
    public double EnemyMax = 1;

    public int TotalWealth = 1;
    public int WaveNum = 0;

    public bool WaveStart;

    GameObject spawnedEnemy;
    public GameObject enemy;

    public Vector3[] EnemySpawnPositions;

    Vector3 EnemySpawnStart;
    Vector3 EnemySpawnSpot;
    LineRenderer StartingEnemyPath;
    LineRenderer enemyPath;

    public LineRenderer[] enemyPaths;

    //private GameObject SpawnedEnemy;

    public int cooldown;

    static public WaveCode self;
    public UnityEvent waveStarted;

    //(This is made by Dante Jones)
    //Diffrent music for diffrent parts of the game
    public AudioSource buildMusic;
    public AudioSource battleMusic;

    public TextMeshProUGUI waveText;
    bool enemiesStartedSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep Game alive
        WaveStart = false;
        DontDestroyOnLoad(this);
        StartCoroutine(Spawner(cooldown));
      }

    private void Awake()
    {
        self = this;
    }
    // Update is called once per frame
    void Update()
        {
        // After every enemy is defeated, put up the number.
        // Get ready for a new wave
        if (EnemyNum == 0 && WaveStart && enemiesStartedSpawning)
        {
            WaveStart = false;
            enemiesStartedSpawning = false;
            //Turns on building phase music stops battle phase music
            buildMusic.Play();
            battleMusic.Stop();
        }
    }

    public IEnumerator Spawner(float cooldown)
    {
        while(true)
        {
            Debug.Log(WaveStart);
            // With the game starting and the number of enemies being less than max
            if (WaveStart && PhantomEnemyNum < EnemyMax)
            {
                // Spawn the enemies
                cooldown -= PhantomEnemyNum * 0.01f;
                EnemyNum++;
                PhantomEnemyNum++;
                if(PhantomEnemyNum <= 10) 
                {
                    EnemySpawnSpot = EnemySpawnStart;
                    enemyPath = StartingEnemyPath;
                }
                else
                // Have a randomiser to where each enemy spawns
                {
                    int RandomNum = Random.Range(0, 2);
                    EnemySpawnSpot = EnemySpawnPositions[RandomNum];
                    enemyPath = enemyPaths[RandomNum];
                }
                spawnedEnemy = Instantiate(enemy, EnemySpawnSpot, Quaternion.identity);
                spawnedEnemy.GetComponent<KnightScript>().lineRenderer = enemyPath;
                enemiesStartedSpawning = true;
            }

            // Have a cooldown for player to not get flung into the next wave
                yield return new WaitForSeconds(cooldown);
        }
    }

    //Start Next Wave
    public void StartNext()
    {
        WaveNum++;
        EnemyMax *= 2;
        int RandomNum = Random.Range(0, 2);
        EnemySpawnStart = EnemySpawnPositions[RandomNum];
        StartingEnemyPath = enemyPaths[RandomNum];
        WaveStart = true;
        waveText.text = "Wave: " + WaveNum;
        waveStarted.Invoke();
        ////Turns on battle phase music stops building phase music
        buildMusic.Stop();
        battleMusic.Play();
    }




}

