/* Author: Victoria T.
 * Date: 3/16/26
 * 
 * Description: The code for the wave/rounds of the game,
 * will also keep track of A LOT of stuff like enemy number,
 * amount of happiness, and total wealth.*/

using UnityEngine;

public class WaveCode : MonoBehaviour
{

    // Random unknown objects go WHEEEEE
    public int EnemyNum = 0;
    public double EnemyMax = 1;

    public int TotalWealth = 1;
    public int WaveNum = 0;

    private bool WaveEnd = false;
    private bool WaveStart = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Keep Game alive
        DontDestroyOnLoad(this);
    }


    // Update is called once per frame
    void Update()
        {

        // After every enemy is defeated, put up the number.
        // Get ready for a new wave
        if (EnemyNum == 0)
        {
            WaveEnd = true;
            WaveStart = false;

        }

        else
        {
            WaveEnd = false;
            WaveStart = true;
        }

        if (WaveEnd)
        {
            WaveNum += 1;
            EnemyMax = 1.5 * EnemyMax;


            // Add a certain amount of wealth, don't know how much.


        }
    }


}

