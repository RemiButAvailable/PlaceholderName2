/* Author: Victoria Troshkov
 * Date: 3/26/26
 * 
 * Am not doing the tutorial now :/ */
/*
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class TutorialScript : MonoBehaviour
{ //The Firey Abbyss we'll call the Tutorial

    public bool TutorialEnd = false;

    private Vector3 changes;

    Vector2 endPosition;

    public List<TextAsset> NewText = new List<TextAsset>();

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
    }

    private void OnMouseDown()
    {

        for(int i = 0; i < 300; i++) {

        //Have different events happen based on what text it is.

        if (NewText[3])
        {
            changes.x = +30;
        }


            if (NewText[6])
            {

            }

            if (NewText[28])
            {
                TutorialEnd = true;
            }
        }
    }


}

 
            |
            |
            |
            |
            |
            |





 
 
 
 
 
 
 
 
 */


