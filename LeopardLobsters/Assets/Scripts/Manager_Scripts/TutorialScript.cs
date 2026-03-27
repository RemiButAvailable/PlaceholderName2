/* Author: Alex P.
 * Date: 3/26/26
 * 
 * The script that will run the tutorial. 
 * The whole thing could probably run it since it will be step by step so not many new scripts are needed.
 * Will have all the the ways the text will be clicked through and events tied with it */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialScript : MonoBehaviour
{ //The Firey Abbyss we'll call the Tutorial

    public bool TutorialEnd = false;

    private int TextNum = 0;

    private Vector3 changes;

    void Start()
    {
        changes = Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialEnd)
        {
            SceneManager.LoadScene("DemoScene");
        }

        //Have different events happen based on what text it is.

        if (TextNum == 3)
        {
            changes.x = +30;
        }


        if (TextNum == 6)
        {


        }
    }

    private void OnMouseDown()
    {

     
        
    }


}
