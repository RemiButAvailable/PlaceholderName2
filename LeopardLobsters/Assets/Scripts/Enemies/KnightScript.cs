using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class KnightScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] waypoints;
    Vector3 direction;

    //num vals;
    public int index;
    public float speed;
    public float defaultSpeed;
    public int damage = 1;
    public int health;

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
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if(index < waypoints.Length)
        {
            direction = waypoints[index] - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
            if(Vector3.Distance(transform.position, waypoints[index]) < 0.1f)
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
            moneyManagerScript.moneyNum += (int)happinessManagerScript.happiness;

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
