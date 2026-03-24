using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public int peopleAtCastle = 5; //starting amount of people
    int peopleTotal;
    [SerializeField] int moneyPerPerson = 5;
    [SerializeField] MoneyManagerScript moneyManager; 
    [SerializeField] TextMeshProUGUI textPeopleTotal;
    [SerializeField] TextMeshProUGUI textPeopleOut;
    //(Made by Dante Jones)
    //The audio for castle being hit
    public AudioSource CastleHitSound;

    public static Castle self;
    private void Awake()
    {
        self = this;
        peopleTotal = peopleAtCastle;
    }
    private void Start()
    {
        textPeopleTotal.text = peopleTotal.ToString();
        textPeopleOut.text = (peopleTotal - peopleAtCastle).ToString();
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

            //DO: lose game stuff vfx sfx transitions whatever

            SceneManager.LoadScene("PeopleLoseScreen");
        }
    }

    public bool personGoesOut() {
        if (peopleAtCastle > 0) { 
            peopleAtCastle--;
            textPeopleOut.text = (peopleTotal - peopleAtCastle).ToString();
            return true;
        }
        return false;
        
    }
    public void personGoesIn() {
        peopleAtCastle++;
        textPeopleOut.text = (peopleTotal - peopleAtCastle).ToString();
    }


    //people making stuff
    public bool inWave = false;
    float timer = 0;
    public int timerMax = 10;
    
    public int minPeopleNeeded = 2; //amount of people that are required to be at castle to make more people
    public int maxPeopleDecrease = 10; //max amount of people that increase speed of timer
    public float percentPerPerson = 1.1f; //the percent multiplied that reduce time for timer

    public Image progressBar; //instantiate in inspector

    private void FixedUpdate()
    {
        if (inWave && peopleAtCastle>=minPeopleNeeded)
        {
            progressBar.fillAmount = timer / timerMax;

            //increase time passed based on people at castle
            timer += Time.deltaTime * Mathf.Pow(percentPerPerson, 
                Mathf.Min(peopleAtCastle,maxPeopleDecrease)-minPeopleNeeded);

            //increases people when timer is done
            if (timer >= timerMax)
            {
                timer = 0;

                //DO: people added SFX VFX

                peopleAtCastle++;
                peopleTotal++;
                textPeopleTotal.text = peopleTotal.ToString();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "knight")
        {
            //Sound that plays when enemy hits castle
            CastleHitSound.Play();
        }
    }

}
