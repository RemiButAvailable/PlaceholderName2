/* Author: Victoria T.
 * Date: 3/16/26
 * 
 * Description: The code for the wave/rounds of the game,
 * will also keep track of stuff like enemy number, and total wealth.*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveCode : MonoBehaviour
{

    // Random unknown objects go WHEEEEE
    public int EnemyNum = 0;
    private int PhantomEnemyNum = 0;
    public double EnemyMax = 1;

    public int TotalWealth = 1;
    public int WaveNum = 0;

    private bool WaveStart = true;

    public GameObject enemy;

    //private GameObject SpawnedEnemy;

    public int cooldown;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep Game alive
        DontDestroyOnLoad(this);

        StartCoroutine(Spawner(cooldown));
    }


    // Update is called once per frame
    void Update()
        {

        // After every enemy is defeated, put up the number.
        // Get ready for a new wave
        if (EnemyNum == 0)
        {
            WaveStart = false;
        }
    }

    public IEnumerator Spawner(int cooldown)
    {
        while(true)
        {
            // With the game starting and the number of enemies being less than max
            if (WaveStart && PhantomEnemyNum < EnemyMax)
            {
                // Spawn the enemies
                EnemyNum++;
                PhantomEnemyNum++;
                Instantiate(enemy);

            }
                yield return new WaitForSeconds(cooldown);
        }


    }

    //Start Next Wave
    public void StartNext()
    {
        WaveNum++;
        EnemyMax *= 1.5;

        WaveStart = true;
    }




}

