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
}
