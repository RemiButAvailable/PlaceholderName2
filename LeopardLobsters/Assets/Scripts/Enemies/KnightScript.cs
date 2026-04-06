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
    [Range(0, 5)]
    public float speed;
    [Range(0, 10)]
    public int damage;

    public int health;
    [Range(0, 100)]
    public int money;

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

    //public objects and lists
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public Vector3[] waypoints;

    //Manager Scripts
    WaveCode waveCode => WaveCode.self;
    MoneyManagerScript moneyManagerScript => MoneyManagerScript.self;
    Happiness_ManagerScript happinessManagerScript=>Happiness_ManagerScript.self;

    //(Made by Dante Jones)
    //The audio for enemy getting hurt
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioResource deathSound;
    [SerializeField] AudioPlayer aPlayerPrefab;
    [SerializeField] float deathSoundVolume = .5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets waypoints to the points along the line renderer
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);
        for(int i = 0; i < waypoints.Length; i++) //sets all waypoint's z pos to 0
        {
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, 0);
        }
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
        }
       
    }
    public void TakeDamage(int dmg) {
        health -= dmg;
        hurtSound.Play();

        //death
        if (health <= 0)
        {
            waveCode.EnemyNum -= 1;

            moneyManagerScript.ChangeMoney(money);

            //sounds
            AudioPlayer aPlayer = Instantiate(aPlayerPrefab);
            aPlayer.playClip(transform.position, deathSound, deathSoundVolume);

            Destroy(gameObject);
        }
    }
    public void ReachedCastle() {
        Destroy(gameObject);
    }
}
