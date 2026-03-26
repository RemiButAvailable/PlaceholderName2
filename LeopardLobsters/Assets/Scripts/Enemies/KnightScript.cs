using UnityEngine;
using System.Collections.Generic;

public class KnightScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] waypoints;
    Vector3 direction;

    //num vals;
    public int index;
    public float speed;
    public int peopleKillOnReachTower = 1;
    public int health;

    //Manager Scripts
    WaveCode waveCode => WaveCode.self;
    MoneyManagerScript moneyManagerScript => MoneyManagerScript.self;
    Happiness_ManagerScript happinessManagerScript=>Happiness_ManagerScript.self;

    //(Made by Dante Jones)
    //The audio for enemy getting hurt
    public AudioSource hurtSound;

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
        //death
        if(health <= 0)
        {
            waveCode.EnemyNum -= 1;
            moneyManagerScript.moneyNum += (int)happinessManagerScript.happiness;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage) {
        health -= damage;
    }
    public void ReachedTower() {
        Destroy(gameObject);
    }
}
