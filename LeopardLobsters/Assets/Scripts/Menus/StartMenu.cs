/* Date: 4/8/26
 * 
 * The script that will help move between scenes, usually is used in the main menu */
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //scene that is gone to when no string is placed
    public string defaultScene; 

    //functions connected through buttons in scene
    public void NextScene(string nextScene) {
        if(nextScene!=null) SceneManager.LoadScene(nextScene);
        else SceneManager.LoadScene(defaultScene);
    }

    public void NextScene() {
        SceneManager.LoadScene(defaultScene);
    }

    public void Quit() {
        Application.Quit();
    }

    // To activate and deactivate the credits on the main menu with the buttons.
    public void Credits(GameObject gameObject)
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }

        else
        {
            gameObject.SetActive(true);
        }


    }
}
