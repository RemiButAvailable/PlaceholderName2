using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public int peopleAtCastle = 2; //starting amount of people
    [SerializeField] int peopleMax = 6;
    [SerializeField] int peopleMaxCost = 30;
    [SerializeField] float peopleMaxCostMult = 2;

    [SerializeField] TowerSelectable towerSelectable;
    [SerializeField] GameObject buttonPanel;
    [SerializeField] TextMeshPro maxPeoplCostButtonText;

    [SerializeField] SpriteRenderer castleSprite;
    [SerializeField] Color tintColor;

    int peopleTotal;
    [SerializeField] int moneyPerPerson = 5;
    [SerializeField] TextMeshProUGUI textPeopleTotal;
    [SerializeField] TextMeshProUGUI textPeopleIn;

    //(Made by Dante Jones)
    //The audio for castle being hit
    public AudioSource castleHitSound;
    [SerializeField] AudioSource PeopleGainSound;

    public static Castle self;

    private void Awake()
    {
        self = this;
        peopleTotal = peopleAtCastle;
    }
    private void Start()
    {
        maxPeoplCostButtonText.text = peopleMaxCost.ToString();
        textUpdatePTotal();
        textUpdatePIn();

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

    public bool personGoesOut() {
        if (peopleAtCastle > 0) {
            peopleAtCastle--;
            textUpdatePIn();
            return true;
        }
        return false;

    }
    public void personGoesIn() {
        peopleAtCastle++;
        textUpdatePIn();
    }


    //people making stuff
    bool inWave => WaveCode.self.WaveStart;
    float timer = 0;
    public int timerMax = 10;

    public int minPeopleNeeded = 2; //amount of people that are required to be at castle to make more people
    public int maxPeopleDecrease = 10; //max amount of people that increase speed of timer
    public float percentPerPerson = 1.1f; //the percent multiplied that reduce time for timer

    [SerializeField] Image progressBar; //instantiate in inspector
    [SerializeField] Image barParent;
    [SerializeField] GameObject barPosition;

    private void FixedUpdate()
    {
        if (peopleAtCastle >= minPeopleNeeded)
        {
            castleSprite.color = Color.white;
        }
        else
        {
            castleSprite.color = tintColor;
            return;
        }
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
        if (other.gameObject.tag == "knight")
        {
            //Sound that plays when enemy hits castle
            castleHitSound.Play();
            KnightScript enemy = other.gameObject.GetComponent<KnightScript>();

            PersonDead(enemy);

            peopleAtCastle -= enemy.damage;
            textUpdatePIn();

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


    public void BuyMaxPeople() { //connected through inspector
        if (!MoneyManagerScript.self.Check(-peopleMaxCost)) return;

        MoneyManagerScript.self.ChangeMoney(-peopleMaxCost);
        peopleMax++;
        peopleMaxCost = (int)( peopleMaxCost * peopleMaxCostMult);
        maxPeoplCostButtonText.text = peopleMaxCost.ToString();
        textUpdatePTotal();
    }

    void textUpdatePTotal() { textPeopleTotal.text = peopleTotal.ToString() +" / "  +peopleMax.ToString(); }
    void textUpdatePIn() { textPeopleIn.text = peopleAtCastle.ToString(); }

    private void Update()
    {
        barParent.rectTransform.position = barPosition.transform.position;
    }

    public void CastleSelected() { }
}
