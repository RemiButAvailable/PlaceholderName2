using UnityEngine;
using TMPro;

public class PauseButtonScript : MonoBehaviour
{
    public GameObject quitToMenuButton;
    bool paused;
    public GameObject fog; //makes the screen muted/have a transparent overlay of color while the game is paused
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
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
            text.text = "Pause Game";
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            quitToMenuButton.SetActive(false);
            fog.SetActive(false);
            text.text = "Resume Game";
            paused = false;
        }
    }
}
