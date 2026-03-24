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
    public int health;

    //Manager Scripts
    WaveCode waveCode;
    MoneyManagerScript moneyManagerScript;
    Happiness_ManagerScript happinessManagerScript;

    //Managers Objs
    GameObject waveManager;
    GameObject moneyManager;
    GameObject happinessManager;

    //(Made by Dante Jones)
    //The audio for enemy getting hurt
    public AudioSource hurtSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);

        //refrences to manager scripts/objects
        waveManager = GameObject.FindWithTag("WaveManager");
        waveCode = waveManager.GetComponent<WaveCode>();

        moneyManager = GameObject.FindWithTag("MoneyManager");
        moneyManagerScript = moneyManager.GetComponent<MoneyManagerScript>();

        happinessManager = GameObject.FindWithTag("HappinessManager");
        happinessManagerScript = happinessManager.GetComponent<Happiness_ManagerScript>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "arrow")
        {
            health -= 1;

            if(health != 0)
            {
                //Plays sound when enemy injured
                hurtSound.Play();
            }
        }
        else if(collision.gameObject.tag == "castle")
        {
            Destroy(gameObject);
        }
    }
}
