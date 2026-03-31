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

    public int TotalWealth = 1;
    public int WaveNum = 0;

    public bool WaveStart;

    GameObject spawnedEnemy;
    public GameObject enemy;
    GameObject bossEnemy;
    public GameObject spawnedBossEnemy;
    public GameObject fastEnemy;
    GameObject selectedEnemy;

    public Vector3[] EnemySpawnPositions;
    public GameObject[] enemies;

    Vector3 EnemySpawnStart;
    Vector3 EnemySpawnSpot;
    LineRenderer StartingEnemyPath;
    LineRenderer enemyPath;

    public LineRenderer[] enemyPaths;

    //private GameObject SpawnedEnemy;


    static public WaveCode self;
    public UnityEvent waveStarted;

    //(This is made by Dante Jones)
    //Diffrent music for diffrent parts of the game
    public AudioSource buildMusic;
    public AudioSource battleMusic;

    public TextMeshProUGUI waveText;
    bool enemiesStartedSpawning;

    bool endedWave;

    [Range(0, 12)]
    public int enemyClumpSizeRandomness;
    [Range (0, 12)]
    public float timeBetweenEnemySpawnsRandomness;
    [Range (0, 12)]
    public float enemySpawnPosOffsetRandomness;
    [Range(0, 1)]
    public float cooldownWithinClump;
    [Range (0, 12)]
    public int probOfFastEnemyDeterminer;
    [Range (0, 60)]
    public double EnemyMax;
    [Range (0, 12)]
    public int cooldown;

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
        if (EnemyNum == 0 && WaveStart && enemiesStartedSpawning && PhantomEnemyNum == EnemyMax && endedWave == false)
        {
            EndWave();
        }
    }

    public IEnumerator Spawner(float cooldown)
    {
        while(true)
        {
            // With the game starting and the number of enemies being less than max
            if (WaveStart /*&& PhantomEnemyNum < EnemyMax*/)
            {
                // Spawn the enemies
                cooldown -= PhantomEnemyNum * 0.01f;
                probOfFastEnemyDeterminer -= 1;
                EnemyNum++;
                PhantomEnemyNum++;
                if(PhantomEnemyNum <= 10) 
                {
                    EnemySpawnSpot = EnemySpawnStart;
                    enemyPath = StartingEnemyPath;
                    selectedEnemy = enemies[0];
                }
                else
                // Have a randomiser to where each enemy spawns
                {
                    int RandomNum = Random.Range(0, 3);
                    EnemySpawnSpot = EnemySpawnPositions[RandomNum];
                    enemyPath = enemyPaths[RandomNum];
                    int RandomNumTwo = Random.Range(0, probOfFastEnemyDeterminer);
                    if(RandomNumTwo >= 10)
                    {
                        RandomNumTwo = 0;
                    }
                    else
                    {
                        RandomNumTwo = 1;
                    }
                    selectedEnemy = enemies[RandomNumTwo];
                }
                int enemyClumpSize = Random.Range(1, enemyClumpSizeRandomness);
                for(int i = 0; i <= enemyClumpSize; i++)
                {
                    float enemySpawnPosOffsetFloat = Random.Range(-enemySpawnPosOffsetRandomness, enemySpawnPosOffsetRandomness);
                    Vector3 offsetEnemySpawnPos = new Vector3(EnemySpawnSpot.x + enemySpawnPosOffsetFloat, EnemySpawnSpot.y + enemySpawnPosOffsetFloat, 0);
                    spawnedEnemy = Instantiate(/*enemy*/ selectedEnemy, offsetEnemySpawnPos, Quaternion.identity);
                    spawnedEnemy.GetComponent<KnightScript>().lineRenderer = enemyPath;
                    spawnedEnemy.GetComponent<KnightScript>().offset = new Vector3(enemySpawnPosOffsetFloat, enemySpawnPosOffsetFloat, 0);
                    yield return new WaitForSeconds(cooldownWithinClump);
                }
                enemiesStartedSpawning = true;
                if (PhantomEnemyNum > 20 && PhantomEnemyNum < 21)
                {
                    spawnedBossEnemy = Instantiate(bossEnemy, EnemySpawnSpot, Quaternion.identity);
                }
            }
            float cooldownMultiplier = Random.Range(1/timeBetweenEnemySpawnsRandomness, timeBetweenEnemySpawnsRandomness);
            // Have a cooldown for player to not get flung into the next wave
                yield return new WaitForSeconds(cooldown * cooldownMultiplier);
        }
    }

    //Start Next Wave
    public void StartNext()
    {
        if(WaveStart == false)
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
            endedWave = false;
        }
    }
    public void EndWave()
    {
        WaveStart = false;
        enemiesStartedSpawning = false;
        //Turns on building phase music stops battle phase music
        buildMusic.Play();
        battleMusic.Stop();
        endedWave = true;
    }
}

