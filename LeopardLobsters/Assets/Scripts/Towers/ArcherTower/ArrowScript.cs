using System.Collections;
using NUnit;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    
    [SerializeField] float timerMax;
    float timer;

    [SerializeField] int damage;

    public Vector2 start;
    public Vector2 target;
    [SerializeField] float yTargetableDis; //y distance from the target that the arrow can hit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 direction = (target - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(start, target, timer / timerMax);
        if (timer >= timerMax) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "knight")
        {
            //checks if collision is 
            if (collision.gameObject.transform.position.y > target.y + yTargetableDis) return;

            collision.GetComponent<KnightScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
