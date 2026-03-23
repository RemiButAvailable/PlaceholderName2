using UnityEngine;
using UnityEngine.Events;

public class TestDetector : MonoBehaviour
{
    public UnityEvent<TestEnemy> EnemyEnter;
    public UnityEvent<TestEnemy> EnemyExit;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            TestEnemy enemy = other.GetComponent<TestEnemy>();
            enemy.OnDeath.AddListener(CallEnemyExit);
            EnemyEnter.Invoke(enemy);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "knight")
        {
            EnemyExit.Invoke(other.GetComponent<TestEnemy>());
        }
    }

    void CallEnemyExit(TestEnemy enemy) {
        EnemyExit.Invoke(enemy);
    }
}

