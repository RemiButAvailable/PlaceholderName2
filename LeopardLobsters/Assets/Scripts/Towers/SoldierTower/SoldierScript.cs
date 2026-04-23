using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoldierScript : MonoBehaviour
{
    //soldier state and tower bools
    //[HideInInspector]
    public bool engaged;
    [HideInInspector]
    public bool fighting;
    [HideInInspector]
    bool atStation;
    [HideInInspector]
    bool isDying;
    [HideInInspector]
    public bool isActive; //is the tower active

    //stat vals that can be edited in the inspector
    [Range(0f, 12f)]
    public float speed;
    [Range (0f, 12f)]
    public float health;

    //Refrences and Vector3s
    public Vector3 stationPosition;
    public GameObject target;
    Vector3 direction;
    public GameObject soldierTower;
    SoldierTowerScript soldierTowerScript;
    BaseTower baseTowerScript;
    GameObject castleObj;
    KnightScript knightScript;
    public WaveCode waveCode;

    [SerializeField] Animator anim;
    [SerializeField] Animator knightAnim;    

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
        soldierTowerScript = soldierTower.GetComponent<SoldierTowerScript>();
        anim.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (engaged == true && target != null)
        {
            anim.SetBool("isIdle", false);
            direction = target.transform.position - transform.position;
            direction.Normalize();
            if (fighting == false)
            {
                transform.position += direction * speed * Time.deltaTime;
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            direction = stationPosition - transform.position;
            direction.Normalize();
            if (atStation == false)
                transform.position += direction * speed * Time.deltaTime;
            else
                anim.SetBool("isIdle", true);
        }

        if (target != null)
        {
            engaged = true;
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                fighting = true;
                target.GetComponent<KnightScript>().speed = 0;
            }
            /*else if (Vector3.Distance(transform.position, stationPosition) < 0.1f && engaged == false)
            {
                atStation = true;
            }*/
        }
        else
        {
            engaged = false;
        }

        if (health <= 0)
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDead", true);
        }

        if(fighting == true && target == null)
        {
            soldierTowerScript.enemiesInZone.Sort();
            foreach(var enemy in soldierTowerScript.enemiesInZone)
            {
                if(enemy.GetComponent<KnightScript>().targeted == false)
                {
                    target = enemy;
                    engaged = true;
                    fighting = false;
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", true);
                }
            }
        }
    }
    IEnumerator FightEnemy()
    {
        while (true)
        {
            if(fighting == true)
            {
                if(target != null)
                knightScript = target.GetComponent<KnightScript>();

                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);

                if(target != null)
                {
                    knightScript.move.SetBool("isWalking", false);
                    knightScript.move.SetBool("isAttacking", true);
                }

                hitSound.Play();

                if(target != null)
                {
                    knightScript.TakeDamage(1);
                }
                yield return new WaitForSeconds(1);
                hitSound.Play();
                health -= 1;
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }
    public void Die()
    {
        AudioPlayer aPlayer = Instantiate(aSoundPrefab);
        aPlayer.playClip(transform.position, deathSound, deathSoundVolume);
        if (target != null)
        {
            target.GetComponent<KnightScript>().speed = target.GetComponent<KnightScript>().defaultSpeed;
            target.GetComponent<KnightScript>().move.SetBool("isAttacking", false);
            target.GetComponent<KnightScript>().move.SetBool("isWalking", true);
            target.GetComponent<KnightScript>().targeted = false;
        }
        soldierTowerScript.RemoveSoldier(this.gameObject);
        Destroy(gameObject);
    }

    public void OnNewWave()
    {
        anim.SetBool("isIdle", true);
    }
}
