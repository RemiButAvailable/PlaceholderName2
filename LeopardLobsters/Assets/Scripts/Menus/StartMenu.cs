using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public string nextScene; 

    //connected through button in scene
    public void NextScene() {
       SceneManager.LoadScene(nextScene);
    }
    
}
