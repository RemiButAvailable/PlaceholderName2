/* Author: Victoria T.
 * Date: 3/16/26
 * 
 * Description: The way the player will place their wonderfull towers*/
using UnityEngine;

public class Placement : MonoBehaviour {

    private bool DragnDrop = false;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (DragnDrop)
        {
            //Have tower follow mouse and be where it was dropped.
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
                // Grab UI of Tower
             
    }

    private void OnMouseDown()
    {

            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DragnDrop = true;   
    }

    private void OnMouseUp()
    {
        DragnDrop = false;
        //Destroy(gameObject);
    }

    // Make sure tower is unable to be picked back up


}

