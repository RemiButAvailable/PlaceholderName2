using UnityEngine;
using System.Collections;

public class SoldierScript : MonoBehaviour
{
    public Vector3 stationPosition;
    public bool engaged;
    public GameObject target;
    Vector3 direction;
    float speed;
    public bool fighting;
    public float health;
    GameObject Tower;
    bool atStation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FightEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if(engaged == true)
        {
            direction = transform.position - target.transform.position;
            direction.Normalize();
            if (fighting == false)
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            direction = transform.position - stationPosition;
            direction.Normalize();
            if(atStation == false)
            transform.position += direction * speed * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            fighting = true;
        }
        else if (Vector3.Distance(transform.position, target.transform.position) < 1f && engaged == false)
        {
            atStation = true;
        }

        if(health == 0)
        {
            Tower.GetComponent<SoldierTowerScript>().RemoveSoldier(this.gameObject);
            Destroy(gameObject);
        }
    }
    IEnumerator FightEnemy()
    {
        while(true)
        {
            if(fighting == true)
            {
                //attack animation?
                target.GetComponent<KnightScript>().health -= 1;
                yield return new WaitForSeconds(1);
                //enemy attack animation?
                health -= 1;
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }
}
