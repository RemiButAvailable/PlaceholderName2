/* Author: Victoria T. (And the other two)
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

    //Vals that are public but aren't meant to be edited in the inspector

    [HideInInspector]
    public int EnemyNum = 0; //number of enemies in the scene
    [HideInInspector]
    public int WaveNum = 0;
    [HideInInspector]
    public int buildNum = 1;
    [HideInInspector]
    public bool WaveStart;
    [HideInInspector]
    public bool endedWave; //keeps the end wave function from running contiguously
    [SerializeField] public int TotalWealth = 1;

    //Private vals
    private int PhantomEnemyNum = 0; //amount of enemies that have spawned since the wave started
    private int order;

    //Prefabs
    public GameObject bossEnemy;
    GameObject selectedEnemy; //(not a prefab but it stores the selected prefab)

    //UI
    public GameObject percentOfWaveKilledBar;

    //spawned Objects
    GameObject spawnedEnemy;
    GameObject spawnedBossEnemy;

    //arrays
    public Vector3[] EnemySpawnPositions;
    public GameObject[] enemies;
    public LineRenderer[] enemyPaths;

    //Spawn Los
    Vector3 EnemySpawnStart;//the location enemies spawn at until phantom enemy num is big enough
    Vector3 EnemySpawnSpot;
    LineRenderer StartingEnemyPath;//the path enemies march along until phantom enemy num is big enough
    LineRenderer enemyPath;

    static public WaveCode self;
    public UnityEvent waveStarted;

    //(This is made by Dante Jones)
    //Diffrent music for diffrent parts of the game
    public AudioSource buildMusic;
    public AudioSource battleMusic;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI buildPhaseText;
    bool enemiesStartedSpawning;

    //vals that can be edited in the inspector
    [Range(0, 12)]
    public int enemyClumpSizeRandomness;
    [Range (0, 12)]
    public float timeBetweenEnemySpawnsRandomness;//cooldown between clumps randomizer
    [Range (0, 12)]
    public float enemySpawnPosOffsetRandomness;
    [Range(0, 1)]
    public float cooldownWithinClump;
    [Range (0, 12)]
    public int probOfFastEnemyDeterminer;//upper limit of the range for the random val that determines if an enemy is fast. Decreases once they start spawning
    [Range (0, 60)]
    public int EnemyMax;
    [Range (0, 12)]
    public int cooldown;//cooldown between clumps
    [Range(0, 12)]
    public int phantomEnemyNumBeforeAltEnemies;//the amount of normal enemies that can spawn before there's a chance of fast ones and bosses
    [Range(0, 12)]
    public int enemyMaxMultiplier;
    [Range(0, 50)]
    public int pointAtWhichBossSpawns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep Game alive
        WaveStart = false;
        waveText.text = "Wave (Dormant): " + WaveNum + " Build (Active): " + buildNum;
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
            if (WaveStart && PhantomEnemyNum < EnemyMax)
            {
                cooldown -= PhantomEnemyNum * 0.01f;//cooldown decreases over the course of the wave as the amount of enemies increases

                if(PhantomEnemyNum > phantomEnemyNumBeforeAltEnemies && probOfFastEnemyDeterminer > 4 /*min amount probOfFastEnemyDeterminerCanBe*/)//if a certain amount of enemies have spawned and the prob of fast enemy determiner hasn't decreased too much, decrease it
                probOfFastEnemyDeterminer -= 1;

                if(PhantomEnemyNum <= phantomEnemyNumBeforeAltEnemies) 
                {
                    EnemySpawnSpot = EnemySpawnStart;
                    enemyPath = StartingEnemyPath;
                    selectedEnemy = enemies[0];
                }
                else// if a certain amount of enemies have spawned, select a spawn spot for the clump 
                {
                    int RandomNum = Random.Range(0, 3);
                    EnemySpawnSpot = EnemySpawnPositions[RandomNum];
                    enemyPath = enemyPaths[RandomNum];
                }
                int enemyClumpSize = Random.Range(1, enemyClumpSizeRandomness + 1);
                if(enemyClumpSize > EnemyMax - PhantomEnemyNum)
                {
                    enemyClumpSize = EnemyMax - PhantomEnemyNum;
                }
                for(int i = 0; i <= enemyClumpSize; i++)
                {
                    //select an enemy type. The prob of getting a fast one increases over the course of the game
                    int RandomNumTwo = Random.Range(0, 1 + probOfFastEnemyDeterminer);
                    if(RandomNumTwo >= 1)
                    RandomNumTwo = 1;

                    selectedEnemy = enemies[RandomNumTwo];

                    //spawn an enemy at the clump's shared starting point with a randomized offset
                    float enemySpawnPosOffsetFloat = Random.Range(-enemySpawnPosOffsetRandomness, enemySpawnPosOffsetRandomness);
                    Vector3 offsetEnemySpawnPos = new Vector3(EnemySpawnSpot.x + enemySpawnPosOffsetFloat, EnemySpawnSpot.y + enemySpawnPosOffsetFloat, 0);

                    //spawn a new enemy
                    spawnedEnemy = Instantiate(selectedEnemy, offsetEnemySpawnPos, Quaternion.identity);
                    KnightScript knightScript = spawnedEnemy.GetComponent<KnightScript>();
                    knightScript.lineRenderer = enemyPath;
                    knightScript.offset = new Vector3(enemySpawnPosOffsetFloat, enemySpawnPosOffsetFloat, 0);
                    knightScript.order = PhantomEnemyNum;

                    EnemyNum++;
                    PhantomEnemyNum++;
                    yield return new WaitForSeconds(cooldownWithinClump);
                }
                enemiesStartedSpawning = true;

                if (PhantomEnemyNum > pointAtWhichBossSpawns && PhantomEnemyNum < pointAtWhichBossSpawns + 1)//spawn boss
                {
                    spawnedBossEnemy = Instantiate(bossEnemy, EnemySpawnSpot, Quaternion.identity);
                }
            }
            float cooldownMultiplier = Random.Range(1/timeBetweenEnemySpawnsRandomness, timeBetweenEnemySpawnsRandomness);
            yield return new WaitForSeconds(cooldown * cooldownMultiplier);
        }
    }

    //Start Next Wave
    public void StartNext()
    {
        if(WaveStart == false)
        {
            WaveNum++;
            PhantomEnemyNum = 0;
            //EnemyMax *= enemyMaxMultiplier;

            int RandomNum = Random.Range(0, 2);
            EnemySpawnStart = EnemySpawnPositions[RandomNum];
            StartingEnemyPath = enemyPaths[RandomNum];

            WaveStart = true;
            waveText.text = "Wave (Active): " + WaveNum + " Build (Dormant): " + buildNum;
            
            //Turns on battle phase music stops building phase music
            buildMusic.Stop();
            battleMusic.Play();
            endedWave = false;
        }
    }
    public void EndWave()
    {
        buildNum++;
        WaveStart = false;
        enemiesStartedSpawning = false;
        //Turns on building phase music stops battle phase music
        buildMusic.Play();
        battleMusic.Stop();
        //buildPhaseText.enabled = true;
        waveText.text = "Wave (Dormant): " + WaveNum + " Build (Active): " + buildNum;
        endedWave = true;
    }
}

