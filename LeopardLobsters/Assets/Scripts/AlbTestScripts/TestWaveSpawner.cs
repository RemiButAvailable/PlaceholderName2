using UnityEngine;

public class TestWaveSpawner : MonoBehaviour
{
    [SerializeField]LineRenderer[] Paths;

    [SerializeField] TestEnemy prefab;

    [SerializeField] float timerMax = 10;
    float timer = 5;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax;
            timerMax = Mathf.Max(timerMax * .85f,1);
            TestEnemy enemy = Instantiate(prefab);
            LineRenderer path = Paths[Random.Range(0, Paths.Length)];
            enemy.transform.position = path.GetPosition(0);
            enemy.lineRenderer = path;
        }
    }
}
