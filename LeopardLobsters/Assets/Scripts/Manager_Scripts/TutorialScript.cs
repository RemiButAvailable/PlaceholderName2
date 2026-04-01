/* Author: Victoria Troshkov
 * Date: 3/26/26
 * 
 * The script that will run the tutorial. 
 * The whole thing could probably run it since it will be step by step so not many new scripts are needed.
 * Will have all the the ways the text will be clicked through and events tied with it */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class TutorialScript : MonoBehaviour
{ //The Firey Abbyss we'll call the Tutorial

    public bool TutorialEnd = false;

    private Vector3 changes;

   // public Text uiText;

    Vector2 endPosition;

    public List<TextAsset> NewText = new List<TextAsset>();

    void Start()
    {
        changes = Camera.main.transform.position;
        //StartCoroutine(moveText());

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
