/*
* Name: KnightScript.cs
* Authors: Remi de Plater, Albert Tan
* Email: remi.deplater@digipen.edu
* Desc: Knight functionality
*
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class KnightScript : MonoBehaviour, IComparable<KnightScript>
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
    [HideInInspector]
    bool isDead;

    //public objects and lists
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public Vector3[] waypoints;
    //public GameObject detectionObj;
    [HideInInspector]
    public UnityEvent<KnightScript> onDeath;

    //Manager Scripts
    WaveCode waveCode => WaveCode.self;
    MoneyManagerScript moneyManagerScript => MoneyManagerScript.self;
    Happiness_ManagerScript happinessManagerScript => Happiness_ManagerScript.self;

    //Animation :D
    public Animator move;

    //(Made by Dante Jones)
    //The audio for enemy getting hurt
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource ArrowHitSound;
    [SerializeField] AudioResource deathSound;
    [SerializeField] AudioResource hitDeathSound;

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
        move.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) speed = .1f;
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
            //detectionObj.transform.position = transform.position + direction * detectionObjDistFromKnight;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //detectionObj.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    //Knight loses health
    public void TakeDamage(int dmg) {
        health -= dmg;
        hurtSound.Play();
        ArrowHitSound.Play();

        //death
        if (health <= 0)
        {
            onDeath.Invoke(this);
            move.SetBool("isWalking", false);
            move.SetBool("isDead", true);
            isDead = true;
        }
    }

    //Knight reaches castle
    public void ReachedCastle() {
        if(waveCode.EnemyNum > 0)
        waveCode.EnemyNum -= 1;

        Destroy(gameObject);
    }

    //albert test code
    [SerializeField] float minLRNodeDistance;
    [SerializeField] float timeCheckPass;
    public int compareIndex => (lineRenderer.positionCount - index + (int)(speed * timeCheckPass / minLRNodeDistance));

    public int CompareTo(KnightScript other) {
        if(other.compareIndex < compareIndex) return 1;
        if (other.compareIndex > compareIndex) return -1;
        return 0;
    }

    //Knight dies
    public void Die()
    {
        if (waveCode.EnemyNum > 0)
            waveCode.EnemyNum -= 1;

        moneyManagerScript.ChangeMoney(money);
        //sounds
        AudioPlayer aDeathSound = Instantiate(aPlayerPrefab);
        AudioPlayer aHitSound = Instantiate(aPlayerPrefab);
        aHitSound.playClip(transform.position, hitDeathSound, deathSoundVolume);
        aDeathSound.playClip(transform.position, deathSound, deathSoundVolume);

        Destroy(gameObject);
    }
}
