using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "knight")
        {
            collision.GetComponent<KnightScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
