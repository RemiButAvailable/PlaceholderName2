using UnityEngine;

public class ArcherTowerScript : MonoBehaviour
{
    public GameObject AttackZone;
    List<GameObject> queue;
    public float cooldown;
    public GameObject Arrow;
    public bool enemyInZone = false;
    Vector3 direction;
    ArrowScript arrowScript;
    GameObject spawnedArrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        queue = new List<GameObject>();
        StartCoroutine(ShootArrows());
    }

    // Update is called once per frame
    void Update()
    {
        if(queue.Count > 0)
        {
            enemyInZone = true;
        }
    }
    public IEnumerator ShootArrows()
    {
        while(true)
        {
            if(enemyInZone)
            {
                spawnedArrow = Instantiate(Arrow, transform.position, Quaternion.identity);
                arrowScript = spawnedArrow.GetComponent<arrowScript>();
                arrowScript.direction = queue[0].transform.position - transform.position; //placeholder direction
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
