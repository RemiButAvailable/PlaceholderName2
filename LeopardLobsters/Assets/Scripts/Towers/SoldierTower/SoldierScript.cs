//using System.Collections;
//using UnityEngine;
//using UnityEngine.Audio;

//public class SoldierScript : MonoBehaviour
//{
//    //soldier state and tower bools
//    //[HideInInspector]
//    public bool engaged;
//    [HideInInspector]
//    public bool fighting;
//    //[HideInInspector]
//    public bool atStation;
//    [HideInInspector]
//    bool isDying;
//    [HideInInspector]
//    public bool isActive;//is the tower active

//    //stat vals that can be edited in the inspector
//    [Range(0f, 12f)]
//    public float speed;
//    [Range (0f, 12f)]
//    public float health;

//    //Refrences and Vector3s
//    public Vector3 stationPosition;
//    public GameObject target;
//    Vector3 direction;
//    public GameObject soldierTower;
//    SoldierTowerScript soldierTowerScript;
//    BaseTower baseTowerScript;
//    GameObject castleObj;
//    KnightScript knightScript;
//    public WaveCode waveCode;

//    //Animation
//    [SerializeField] Animator anim;
//    [SerializeField] Animator knightAnim;    

//    //This was made by Dante Jones
//    [SerializeField] AudioSource hitSound;
//    [SerializeField] AudioResource deathSound;
//    [SerializeField] AudioPlayer aSoundPrefab;
//    [SerializeField] float deathSoundVolume = .5f;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        StartCoroutine(FightEnemy());
//        //StartCoroutine(Printer());

//        engaged = false;

//        //set script refrences
//        baseTowerScript = GetComponent<BaseTower>();
//        soldierTowerScript = soldierTower.GetComponent<SoldierTowerScript>();

//        anim.SetBool("isIdle", true);
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        if (target != null)//if the soldier has a target
//        {
//            engaged = true;

//            anim.SetBool("isIdle", false);

//            direction = target.transform.position - transform.position;
//            direction.Normalize();

//            if (fighting == false)//if the soldier is gaining on the target
//            {
//                transform.position += direction * speed * Time.deltaTime;
//                anim.SetBool("isWalking", true);
//                anim.SetBool("isAttacking", false);
//            }

//            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)//if the soldier is in close range of its target
//            {
//                fighting = true;
//                target.GetComponent<KnightScript>().speed = 0;
//            }
//        }
//        else//if the soldier doesn't have a target
//        {
//            engaged = false;
//            direction = stationPosition - transform.position;
//            direction.Normalize();

//            if (atStation == false)//if the soldier isn't back at its station
//            {
//                transform.position += direction * speed * Time.deltaTime;
//            }
//            else
//            {
//                anim.SetBool("isIdle", true);
//            }
//        }

//        if (Vector3.Distance(transform.position, stationPosition) < 0.1f && engaged == false)//if the soldier returns to its staion
//        {
//            atStation = true;
//            anim.SetBool("isIdle", true);
//        }
//        else
//        {
//            atStation = false;
//        }

//        if (health <= 0)//if the soldier should die
//        {
//            anim.SetBool("isAttacking", false);
//            anim.SetBool("isWalking", false);
//            anim.SetBool("isIdle", false);
//            anim.SetBool("isDead", true);
//        }

//        if (fighting == true && target == null)//if the soldier's target dies
//        {
//            fighting = false;

//            anim.SetBool("isAttacking", false);
//            anim.SetBool("isWalking", true);

//            //target a new enemy
//            soldierTowerScript.enemiesInZone.Sort();
//            foreach (var enemy in soldierTowerScript.enemiesInZone)
//            {
//                if (enemy.GetComponent<KnightScript>().targeted == false)
//                {
//                    target = enemy;
//                    engaged = true;
//                }
//            }
//        }
//    }
//    IEnumerator FightEnemy()
//    {
//        while (true)
//        {
//            if(fighting == true && target != null)
//            {
//                knightScript = target.GetComponent<KnightScript>();

//                knightScript.move.SetBool("isWalking", false);
//                knightScript.move.SetBool("isAttacking", true);

//                anim.SetBool("isAttacking", true);
//                anim.SetBool("isWalking", false);

//                knightScript.TakeDamage(1);
                
//                hitSound.Play();

//                yield return new WaitForSeconds(1);

//                health -= 1;

//                hitSound.Play();

//                yield return new WaitForSeconds(1);
//            }
//            yield return null;
//        }
//    }
//    public void Die()
//    {
//        AudioPlayer aPlayer = Instantiate(aSoundPrefab);
//        aPlayer.playClip(transform.position, deathSound, deathSoundVolume);
//        if (target != null)
//        {
//            KnightScript knightScript = target.GetComponent<KnightScript>();

//            knightScript.move.SetBool("isAttacking", false);
//            knightScript.move.SetBool("isWalking", true);

//            knightScript.speed = target.GetComponent<KnightScript>().defaultSpeed;
//            knightScript.targeted = false;
//        }
//        soldierTowerScript.RemoveSoldier(this.gameObject);
//        Destroy(gameObject);
//    }

//    public void OnNewWave()
//    {
//        anim.SetBool("isIdle", true);
//    }

//    public IEnumerator Printer()
//    {
//        while (true)
//        {
//            Debug.Log("isIdle " + anim.GetBool("isIdle"));
//            Debug.Log("isWalking " + anim.GetBool("isWalking"));
//            Debug.Log("isAttacking " + anim.GetBool("isAttacking"));
//            Debug.Log("isDead " + anim.GetBool("isDead"));
//            yield return new WaitForSeconds(0.5f);
//        }
//    }
//}
