using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
