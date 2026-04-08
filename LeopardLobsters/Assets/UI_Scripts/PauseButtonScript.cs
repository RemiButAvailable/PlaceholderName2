using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseButtonScript : MonoBehaviour
{
    public GameObject quitToMenuButton;
    bool paused;
    public GameObject fog; //makes the screen muted/have a transparent overlay of color while the game is paused
    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseOrResumeGame()
    {
        if(!paused)
        {
            Time.timeScale = 0;
            quitToMenuButton.SetActive(true);
            fog.SetActive(true);
            text.text = "Resume Game";
            paused = true;
        }
        else
        {
            Debug.Log("ah");
            Time.timeScale = 1;
            quitToMenuButton.SetActive(false);
            fog.SetActive(false);
            text.text = "Pause Game";
            paused = false;
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
