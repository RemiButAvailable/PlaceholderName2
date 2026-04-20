using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoldierScript : MonoBehaviour
{
    //soldier state bools
    [HideInInspector]
    public bool engaged;
    [HideInInspector]
    public bool fighting;
    [HideInInspector]
    bool atStation;

    //stat vals that can be edited in the inspector
    [Range(0f, 12f)]
    public float speed;
    [Range (0f, 12f)]
    public float health;

    //Refrences and Vector3s
    public Vector3 stationPosition;
    public GameObject target;
    Vector3 direction;
    public GameObject Tower;
    BaseTower baseTowerScript;

    [SerializeField] Animator anim;

    public bool isActive; //is the tower active    

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
        baseTowerScript = GetComponent<BaseTower>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isActive)
        {*/
            if (engaged == true && target != null)
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
                if (atStation == false)
                    transform.position += direction * speed * Time.deltaTime;
            }

            if (target != null)
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

            if (health <= 0)
            {
                //Will add volume
                AudioPlayer aPlayer = Instantiate(aSoundPrefab);
                aPlayer.playClip(transform.position, deathSound, deathSoundVolume);
                target.GetComponent<KnightScript>().speed = target.GetComponent<KnightScript>().defaultSpeed;
                target.GetComponent<KnightScript>().targeted = false;
                Tower.GetComponent<SoldierTowerScript>().RemoveSoldier(this.gameObject);
                Destroy(gameObject);
            }

            if (target == null && fighting == true)
            {
                foreach (var enemy in Tower.GetComponent<SoldierTowerScript>().enemiesInZone)
                {
                    if (enemy.GetComponent<KnightScript>().targeted == false)
                    {
                        target = enemy;
                    }
                }
                fighting = false;

                if (target != null)
                    engaged = true;
            }
        //}
    }
    IEnumerator FightEnemy()
    {
        anim.Play("Soldier_Attack");

        while (true)
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
            //anim.Play("Soldier_Idle");
            yield return null;
        }
    }
}
