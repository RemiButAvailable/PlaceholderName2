using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class TestFountain : MonoBehaviour
{
    Happiness_ManagerScript hManager;
    int towerCount = 0;

    [SerializeField] float happyPerTower = .05f;
    [SerializeField] float cooldown = 1;

    float timer = 0;

    public bool active = false;
    [SerializeField]BaseTower baseTower;

    private void Start()
    {
        //hManager = Happiness_ManagerScript.self
        baseTower.isActive.AddListener(SetActive);
    }

    void TowerEnter(Collider2D other)
    {
            towerCount++;
    }
    void TowerExit(Collider2D other)
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
            timer = cooldown;

            //happiness change sfx vfx
        }
    }

    void SetActive(bool towerActive) { active = towerActive; }
}

