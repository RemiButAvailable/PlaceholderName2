using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class TestFountain : MonoBehaviour
{
    Happiness_ManagerScript hManager;
    int towerCount = 0;

    [SerializeField] float happyPerTower = .05f;
    [SerializeField] float cooldown = 1;

    float timer = 0;

    public bool active = false;

    private void Start()
    {
        //hManager = Happiness_ManagerScript.instance
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "tower")
        {
            towerCount++;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "tower")
        {
            towerCount--;
        }
    }

    private void FixedUpdate()
    {
        if (!active) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = cooldown;

            //happiness change sfx vfx
        }
    }
}

