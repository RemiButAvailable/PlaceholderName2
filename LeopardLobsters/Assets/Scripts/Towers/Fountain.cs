using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class TestFountain : MonoBehaviour
{
    int towerCount = 0;

    [SerializeField] float happyPerTower = .03f;
    [SerializeField] float cooldown = 1f;

    float timer = 0;

    public bool active = false;
    [SerializeField]BaseTower baseTower;

    [SerializeField] TowerAddedChecker checker;
    //(This was made by Dante Jones)
    [SerializeField] AudioSource WaterSound;
    [SerializeField] AudioSource happySound;
    //animation
    [SerializeField] Animator happinessGain;

    Happiness_ManagerScript happyMan => Happiness_ManagerScript.self;

    private void Start()
    {
        //hManager = Happiness_ManagerScript.self
        baseTower.isActive.AddListener(SetActive);
        baseTower.OnPlace.AddListener(CheckTowersInArea);

        baseTower.highlight.Highlighted.AddListener(TowerHighlighted);
        baseTower.highlight.DeHighlighted.AddListener(TowerDehighlighted);

        checker.towerEnter.AddListener(TowerEnter);
        checker.towerExit.AddListener(TowerExit);
    }

    private void FixedUpdate()
    {
        if (!active || !WaveCode.self.WaveStart) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = cooldown + timer;

            //happiness change sfx vfx
            happinessGain.Play("HappinessUp");
            happySound.Play();

            Happiness_ManagerScript.self.ChangeHappy(happyPerTower*towerCount);
        }
    }

    //basetower active event
    void CheckTowersInArea(BaseTower tower) {
        Collider2D col = GetComponent<Collider2D>();

        col.gameObject.layer = LayerMask.NameToLayer("CheckTowerPlacement");

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Tower"));

        Collider2D[] results = new Collider2D[100];

        col.Overlap(filter, results);

        int i = 0;
        while (i<results.Length && results[i] != null) {
            towerCount++;
            i++;
        }

    }

    void SetActive(bool towerActive) { 
        active = towerActive;

        if (!active)
        {
            happyMan.FountainRemove(this);
            WaterSound.Stop();
        }
        else { happyMan.FountainAdd(this); }
    }

    //tower selectable events
    void TowerEnter(BaseTower other)
    {
        towerCount++;
    }
    void TowerExit(BaseTower other)
    {
        towerCount--;
    }

    //tower higlighted events
    void TowerHighlighted()
    {
        if (active)
        {
            WaterSound.Play();
        }
    }
    void TowerDehighlighted()
    {
        WaterSound.Stop();
    }

    public float getHappinessWithTime(float dur) {
        return happyPerTower*towerCount/ (cooldown / dur);
    }
}

