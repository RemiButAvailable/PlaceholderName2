/*
 * 
 * Description: The script for the castle, keeping track of the people there are and if an
 * enemy has hit it. Will also generate more people as time goes on.
 */

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [Header("Editable values")]
    public int peopleAtCastle = 2; //starting amount of people
    int peopleTotal;

    [SerializeField] int peopleMax = 6; //max amount of people can have
    [SerializeField] int peopleMaxCost = 30; //cost to add to people max
    [SerializeField] float peopleMaxCostMult = 2; //cost multiplier each time bought
    [SerializeField] int moneyPerPerson = 5; //money gained after a wave
    [Space]
    public int minPeopleNeeded = 2; //amount of people that are required to be at castle to make more people
    public int maxPeopleDecrease = 10; //max amount of people that increase speed of timer
    public float percentPerPerson = 1.1f; //the percent multiplied that reduce time for timer
    
    [Space][Space]
    [Header("People add progress bar")]
    [SerializeField] Image progressBar; //instantiate in inspector
    [SerializeField] Image barParent;
    [SerializeField] GameObject barPosition;

    [Space][Space]
    [Header("text and objects for castle")]
    [SerializeField] TextMeshProUGUI textPeopleTotal;
    [SerializeField] TextMeshProUGUI textPeopleIn;
    
    [SerializeField] TowerSelectable towerSelectable;


    [Space][Space]
    [Header("button pannel stuff")]
    [SerializeField] TextMeshPro maxPeoplCostButtonText;
    [SerializeField] GameObject buttonPanel;

    [Space][Space]
    [Header("tower active stuff")]
    [SerializeField] SpriteRenderer castleSprite;
    [SerializeField] SpriteRenderer[] peopleSprites;
    [SerializeField] Color tintColor;
    [SerializeField] AudioSource towerActive;
    [SerializeField] AudioSource towerDeactive;

    [Space][Space]
    [Header("CastleHit sounds")]
    [SerializeField] AudioSource castleHitSound;
    [SerializeField] AudioSource PeopleGainSound;

    public static Castle self;

    private void Awake()
    {
        self = this;
        peopleTotal = peopleAtCastle;
        //WaveCode.self.waveEnd.AddListener(endOfWave);
    }
    private void Start()
    {
        // Have the text be the same as the number
        maxPeoplCostButtonText.text = peopleMaxCost.ToString();
        textUpdatePTotal();
        textUpdatePIn();
        progressBar.fillAmount = timer;

        towerSelectable.selected.AddListener(TowerSelected);
        towerSelectable.deSelected.AddListener(TowerDeselected);
    }

    void TowerSelected() { buttonPanel?.SetActive(true); }
    void TowerDeselected() { buttonPanel?.SetActive(false); }


    //adds money based on population
    public void endOfWave() {
        MoneyManagerScript.self.ChangeMoney (peopleAtCastle * moneyPerPerson);

        //DO: add money sfx vfx
    }
    // Everytime a person gets put out to the field, remove from the castle if available
    public bool personGoesOut() {
        if (peopleAtCastle > 0) {

            peopleAtCastle--;
            textUpdatePIn();

            //enabling people sprites
            if (peopleAtCastle < peopleSprites.Length)
            {
                peopleSprites[peopleAtCastle].enabled = false;
            }

            //checking active or not
            if (peopleAtCastle < minPeopleNeeded)
            {
                towerDeactive.Play();
                castleSprite.color = tintColor;
                progressBar.color = tintColor;
            }

            return true;
        }
        return false;

    }
    // When a person gets removed from the field, add to the castle
    public void personGoesIn() {
        //enabling people sprites
        if (peopleAtCastle < peopleSprites.Length)
        {
            peopleSprites[peopleAtCastle].enabled = true;
        }

        peopleAtCastle++;
        textUpdatePIn();

        //checking active
        if (peopleAtCastle >= minPeopleNeeded)
        {
            towerActive.Play();
            castleSprite.color = Color.white;
            progressBar.color = Color.white;
        }
    }


    //people making stuff
    bool inWave => WaveCode.self.WaveStart;
    float timer = 0;
    public int timerMax = 10;

    private void FixedUpdate()
    {
        if (inWave && peopleAtCastle >= minPeopleNeeded && peopleTotal < peopleMax)
        {
            progressBar.fillAmount = timer / timerMax;

            //increase time passed based on people at castle
            timer += Time.deltaTime * Mathf.Pow(percentPerPerson,
                Mathf.Min(peopleAtCastle, maxPeopleDecrease) - minPeopleNeeded);

            //increases people when timer is done
            if (timer >= timerMax)
            {
                timer = 0;

                //DO: people added SFX VFX
                PeopleGainSound.Play();
                peopleAtCastle++;
                peopleTotal++;
                textUpdatePTotal();
                textUpdatePIn();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // The people in castle killer
        if (other.gameObject.tag == "knight")
        {
            //Sound that plays when enemy hits castle
            castleHitSound.Play();
            KnightScript enemy = other.gameObject.GetComponent<KnightScript>();

            PersonDead(enemy);

            peopleAtCastle -= enemy.damage;
            textUpdatePIn();

            // One of the reasons for the loss
            enemy.ReachedCastle();
            if (peopleAtCastle < 0) {
                SceneManager.LoadScene("PeopleLoseScreen");
            }
        }
    }

    public void PersonDead(KnightScript enemy){    
        peopleTotal -= enemy.damage;
        textUpdatePTotal();
    }

    // Increase the max amount of people that can be housed in the castle
    public void BuyMaxPeople() { //connected through inspector
        if (!MoneyManagerScript.self.Check(-peopleMaxCost)) return;

        MoneyManagerScript.self.ChangeMoney(-peopleMaxCost);
        peopleMax++;
        peopleMaxCost = (int)( peopleMaxCost * peopleMaxCostMult);
        maxPeoplCostButtonText.text = peopleMaxCost.ToString();
        textUpdatePTotal();
    }

    // Text stuff
    void textUpdatePTotal() { textPeopleTotal.text = peopleTotal.ToString() +" / "  +peopleMax.ToString(); }
    void textUpdatePIn() { textPeopleIn.text = peopleAtCastle.ToString(); }

    private void Update()
    {
        barParent.rectTransform.position = barPosition.transform.position;
    }

    public void CastleSelected() { }
}
