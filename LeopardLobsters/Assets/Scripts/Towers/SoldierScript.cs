using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoldierScript : MonoBehaviour
{
    public Vector3 stationPosition;
    public bool engaged;
    public GameObject target;
    Vector3 direction;
    public float speed;
    public bool fighting;
    public float health;
    public GameObject Tower;
    bool atStation;
    //This was made by Dante Jones
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioResource deathSound;
    [SerializeField] AudioPlayer aSoundPrefab;
    [SerializeField] float deathSoundVolume = .5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FightEnemy());
        engaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(engaged == true && target != null)
        {
            direction = target.transform.position - transform.position;
            direction.Normalize();
            if (fighting == false)
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            direction = stationPosition - transform.position;
            direction.Normalize();
            if(atStation == false)
            transform.position += direction * speed * Time.deltaTime;
        }

        if(target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 1f)
            {
                fighting = true;
                target.GetComponent<KnightScript>().speed = 0;
            }
            else if (Vector3.Distance(transform.position, stationPosition) < 1f && engaged == false)
            {
                atStation = true;
            }
        }

        if(health <= 0)
        {
            //Will add volume
            AudioPlayer aPlayer = Instantiate(aSoundPrefab);
            aPlayer.playClip(transform.position, deathSound, deathSoundVolume);
            target.GetComponent<KnightScript>().speed = target.GetComponent<KnightScript>().speed;
            target.GetComponent<KnightScript>().targeted = false;
            Tower.GetComponent<SoldierTowerScript>().RemoveSoldier(this.gameObject);
            Destroy(gameObject);
        }

        if(target == null && fighting == true)
        {
            Debug.Log("needs new target");
            foreach(var enemy in Tower.GetComponent<SoldierTowerScript>().enemiesInZone)
            {
                if(enemy.GetComponent<KnightScript>().targeted == false)
                {
                    Debug.Log("new target");
                    target = enemy;
                }
            }
            fighting = false;

            if(target != null)
            engaged = true;
        }
    }
    IEnumerator FightEnemy()
    {
        while(true)
        {
            if(fighting == true)
            {
                //attack animation?
                //play enemy damage sound
                target.GetComponent<KnightScript>().health -= 1;
                yield return new WaitForSeconds(1);
                //play soldier damage sound
                hitSound.Play();
                //enemy attack animation?
                health -= 1;
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }
}
