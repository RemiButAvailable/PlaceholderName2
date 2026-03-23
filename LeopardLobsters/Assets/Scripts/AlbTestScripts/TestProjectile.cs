using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class TestProjectile : MonoBehaviour
{
    public Vector3 start;
    public TestEnemy target;
    Vector3 end;

    [SerializeField] int damage;
    public UnityEvent Effect;
    [SerializeField] bool hasEffect = false;

    [SerializeField] float timerMax;
    float timer;

    void Start()
    {
        timer = timerMax;
    }

    public void Shoot(TestEnemy enemy) {
        start = transform.parent.position;
        target = enemy;
        enemy.DeathPosition.AddListener(DeathPos);
        timer = timerMax;
    }


    private void Update()
    {
        timer -= Time.deltaTime;
        if(target) end = target.transform.position;

        transform.position = Vector3.Lerp(end, start, timer / timerMax);

        //points at target
        Vector2 aDir = (end - transform.position);
        float angle = Mathf.Atan2(aDir.y, aDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //on arrow hit stuff
        if (timer <= 0) {
            if(target != null) target.changeHealth(-damage);
            if (hasEffect) Effect.Invoke();
            else {
                Destroy(gameObject);
            }

        }
    }

    public void DeathPos(Vector3 pos)
    { 
        end = pos; 
    }
}
