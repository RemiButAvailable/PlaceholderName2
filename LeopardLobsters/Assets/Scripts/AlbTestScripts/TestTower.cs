using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TestTower : MonoBehaviour
{
    [SerializeField] TestDetector attackZone;

    List<TestEnemy> queue = new List<TestEnemy>();

    float timer = 0;
    [SerializeField] float cooldown;
    [SerializeField] TestProjectile projectilePrefab;

    TestEnemy target => queue[0];

    public bool active = false;

    void Start()
    {
        attackZone.EnemyEnter.AddListener(EnemyEnter);
        attackZone.EnemyExit.AddListener(EnemyExit);
        GetComponentInParent<BaseTower>().isActive.AddListener(IsActive);
    }

    public void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (queue.Count <= 0 || !active) timer = 0;

        if (queue.Count > 0 && active)
        {
            if (timer <= 0)
            {
                timer = cooldown - timer;

                Instantiate(projectilePrefab, transform).Shoot(target);
            }
        }
    }

    void EnemyEnter(TestEnemy knight)
    {
        queue.Add(knight);
        queue.Sort();

    }
    void EnemyExit(TestEnemy knight)
    {
        queue.Remove(knight);
    }

    void IsActive(bool isTrue)
    {
        active = isTrue;
    }

}


