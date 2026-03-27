using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class TestFountain : MonoBehaviour
{
    int towerCount = 0;

    [SerializeField] float happyPerTower = .05f;
    [SerializeField] float cooldown = 1;

    float timer = 0;

    public bool active = false;
    [SerializeField]BaseTower baseTower;

    [SerializeField] TowerAddedChecker checker;

    private void Start()
    {
        //hManager = Happiness_ManagerScript.self
        baseTower.isActive.AddListener(SetActive);
        baseTower.OnPlace.AddListener(CheckTowersInArea);
        checker.towerEnter.AddListener(TowerEnter);
        checker.towerExit.AddListener(TowerExit);
    }

    void TowerEnter(BaseTower other)
    {
            towerCount++;
    }
    void TowerExit(BaseTower other)
    {
            towerCount--;
    }

    private void FixedUpdate()
    {
        if (!active) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = cooldown - timer;

            //happiness change sfx vfx

            Happiness_ManagerScript.self.ChangeHappy(happyPerTower*towerCount);
        }
    }

    void CheckTowersInArea() {
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

    void SetActive(bool towerActive) { active = towerActive; }
}

