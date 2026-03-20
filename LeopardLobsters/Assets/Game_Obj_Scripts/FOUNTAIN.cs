using UnityEngine;

public class FOUNTAIN : MonoBehaviour
{
    private GameObject NeighborhoodClose;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NeighborhoodClose)
        {
            //Blah Blah Blah

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "knight")
        {
            Destroy(gameObject);
        }
        
    }
}
