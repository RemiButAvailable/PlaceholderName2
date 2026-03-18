using UnityEngine;
using System.Collections.Generic;

public class KnightScript : MonoBehaviour
{
    //public List<Vector3> waypoints;
    public Vector3[] waypoints;
    Vector3 direction;
    public int index;
    public float speed;
    LineRenderer lineRenderer;
    WaveCode waveCode;
    GameObject waveManager;
    public int health;
    GameObject moneyManager;
    MoneyManagerScript moneyManagerScript;
    GameObject happinessManager;
    HappinessManagerScript happinessManagerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);
        waveManager = GameObject.FindWithTag("WaveManager");
        moneyManager = GameObject.FindWithTag("MoneyManager");

        happinessManager = GameObject.FindWithTag("HappinessManager");
        waveCode = waveManager.GetComponent<waveCode>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(health <= 0)
        {
            waveCode.enemyNum -= 1;
            moneyManagerScript.moneyNum += 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "arrow")
        {
            health -= 1;
        }
        else if(collision.gameObject.tag == "castle")
        {
            Destroy(gameObject);
        }
    }
}
