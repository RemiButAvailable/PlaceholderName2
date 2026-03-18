using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public int peopleAtCastle = 5; //starting amount of people
    int peopleTotal;
    public int moneyPerPerson = 5;
    public MoneyManagerScript moneyManager; //instantiate in inspector


    private void Awake()
    {
        peopleTotal = peopleAtCastle;
    }

    //adds money based on population
    public void endOfWave() {
        moneyManager.moneyNum += peopleAtCastle * moneyPerPerson;
        //DO: add money sfx vfx
    }

    //when knight reaches the castle
    public void enemyArrived() {
        peopleAtCastle--;
        if (peopleAtCastle < 0) {
            //DO: lose game stuff 
            SceneManager.LoadScene("PeopleLoseScreen");
        }
    }

    //more people stuff
    public bool inWave = false;
    float timer = 0;
    public int timerMax = 10;
    
    public int minPeopleNeeded = 2; //amount of people that are required to be at castle to make more people
    public int maxPeopleDecrease = 10; //max amount of people that increase speed of timer
    public float percentPerPerson = 1.25f; //the percent multiplied that reduce time for timer

    public Image progressBar; //instantiate in inspector

    private void FixedUpdate()
    {
        if (inWave && peopleAtCastle>=minPeopleNeeded)
        {
            progressBar.fillAmount = timer / timerMax;

            //increase time passed based on people at castle
            timer += Time.deltaTime * Mathf.Pow(percentPerPerson, 
                Mathf.Min(peopleAtCastle,maxPeopleDecrease)-minPeopleNeeded);

            //increases people when timer is done BROKEN DO SOMETHING
            if (timer >= timerMax)
            {
                timer = 0;
                //DO: people added SFX VFX

                peopleAtCastle++;
                peopleTotal++;
            }
        }
    }



}
