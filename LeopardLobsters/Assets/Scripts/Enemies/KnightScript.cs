/*
 * Remi de Plater
 * Knight enemy functionality
 */
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class KnightScript : MonoBehaviour
{
    //vals that can be edited in the inspector
    [Range(0, 12)]
    public float defaultSpeed;
    [Range(0, 12)]
    public int damage;
    [Range(0, 100)]
    public int money;
    [Range(0, 12)]
    public int health;
    [Range(0, 12)]
    public float detectionObjDistFromKnight;//the distance from the knight of the line that checks if an enemy surpassed this enemy

    //vals that are public but not cause they're meant to be edited in the inspector
    [HideInInspector]
    public int index;
    [HideInInspector]
    public bool targeted;
    [HideInInspector]
    public Vector3 offset;
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public Vector3 nextWayPoint;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float order;
    [HideInInspector]
    public ArcherTowerScript inhabitedTowerZone;

    //public objects and lists
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public Vector3[] waypoints;
    public GameObject detectionObj;

    //Manager Scripts
    WaveCode waveCode => WaveCode.self;
    MoneyManagerScript moneyManagerScript => MoneyManagerScript.self;
    Happiness_ManagerScript happinessManagerScript => Happiness_ManagerScript.self;

    //(Made by Dante Jones)
    //The audio for enemy getting hurt
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource ArrowHitSound;
    [SerializeField] AudioResource deathSound;
    
    [SerializeField] AudioPlayer aPlayerPrefab;
    [SerializeField] float deathSoundVolume = .5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets waypoints to the points along the line renderer
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);
        for (int i = 0; i < waypoints.Length; i++) //sets all waypoint's z pos to 0
        {
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, 0);
        }
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if(index < waypoints.Length)
        {
            nextWayPoint = waypoints[index];
            direction = waypoints[index] + offset - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
            if(Vector3.Distance(transform.position, waypoints[index] + offset) < 0.1f)
            {
                index++;
            }
            detectionObj.transform.position = transform.position + direction * detectionObjDistFromKnight;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            detectionObj.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void TakeDamage(int dmg) {
        health -= dmg;
        hurtSound.Play();
        ArrowHitSound.Play();

        //death
        if (health <= 0)
        {
            if(waveCode.EnemyNum > 0)
            waveCode.EnemyNum -= 1;

            moneyManagerScript.ChangeMoney(money);

            //sounds
            AudioPlayer aPlayer = Instantiate(aPlayerPrefab);
            aPlayer.playClip(transform.position, deathSound, deathSoundVolume);
           

            if (inhabitedTowerZone != null)
            inhabitedTowerZone.queue.Remove(this.gameObject);

            Destroy(gameObject);
        }
    }
    public void ReachedCastle() {
        if(waveCode.EnemyNum > 0)
        waveCode.EnemyNum -= 1;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "detectionLine")
        {
            Debug.Log("ah");
            order--;
            collision.gameObject.GetComponentInParent<KnightScript>().order++;
            if(inhabitedTowerZone != null && inhabitedTowerZone.queue[0] == collision.gameObject)
            {
                inhabitedTowerZone.ChangeTarget(collision.gameObject);
            }
        }
    }
}
