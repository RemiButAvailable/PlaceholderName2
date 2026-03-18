using UnityEngine;

public class Castle : MonoBehaviour
{
    public int peopleAtCastle = 0;
    public int peopleTotal = 0;

    public void endOfWave() {
        //add coins based on people

        //idfk
    }

    //when knight reaches the castle
    public void enemyArrived() {
        peopleAtCastle--;
        if (peopleAtCastle < 0) {
            //lose stuff
        }
    }

    //more people stuff
    public bool inWave = false;
    float timer = 0;
    public int timerMax = 10;
    public int minPeopleNeeded = 2; //amount of people that are required to be at castle to make more people
    public int maxPeopleDecrease = 10; //max amount of people that increase speed of timer
    public float percentPerPerson = 1.25f; //the percent multiplied that reduce time for timer

    private void FixedUpdate()
    {
        if (inWave && peopleAtCastle>=minPeopleNeeded)
        {
            //DO: change progress bar

            //increase time passed based on people at castle
            timer -= Time.deltaTime * Mathf.Pow(percentPerPerson, 
                Mathf.Min(peopleAtCastle,maxPeopleDecrease)-minPeopleNeeded);

            //increases people when timer is done
            if (timer < 0)
            {
                timer = timerMax;
                //DO: people added SFX VFX

                peopleAtCastle++;
                peopleTotal++;
            }
        }
    }



}
