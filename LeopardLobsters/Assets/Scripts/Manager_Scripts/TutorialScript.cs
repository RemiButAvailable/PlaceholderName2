/* The script that will run the tutorial. 
 * The whole thing could probably run it since it will be step by step so not many new scripts are needed.
 * Will have all the the ways the text will be clicked through and events tied with it */

using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{ //The Firey Abbyss we'll call the Tutorial

    public bool TutorialEnd = false;

    private int TextNum = 0;

    void Start()
    {
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialEnd)
        {
            SceneManager.LoadScene("DemoScene");
            SceneManager.UnloadSceneAsync("Tutorial I guess");
        }
    }

    private void OnMouseDown()
    {
        
        //Have different events happen based on what text it is.
        if(TextNum == 6)
        {


        }

        
    }


}
