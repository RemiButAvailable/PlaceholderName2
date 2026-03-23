using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : MonoBehaviour, IComparable<TestEnemy>
{
    [SerializeField] LineRenderer lineRenderer;
    Vector3[] waypoints;
    Vector3 direction;

    //num vals;
    public int index;
    [SerializeField] float speed;
    [SerializeField] int health;

    public UnityEvent<TestEnemy> OnDeath;
    public UnityEvent<Vector3> DeathPosition;

    void Start()
    {
        waypoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(waypoints);
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if (index < waypoints.Length)
        {
            direction = waypoints[index] - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, waypoints[index]) < 0.1f)
            {
                index++;
            }
        }
    }

    public void changeHealth(int num) {
        health += num;
        if (health <= 0) {

            OnDeath.Invoke(this);
            DeathPosition.Invoke(transform.position);
            OnDeath.RemoveAllListeners();

            //sfx vfx

            Destroy(gameObject);
        }
    }

    public int CompareTo(TestEnemy other) {
        if(other.index > index) return 1;
        if(other.index < index) return -1;
        return 0;
    }
}

