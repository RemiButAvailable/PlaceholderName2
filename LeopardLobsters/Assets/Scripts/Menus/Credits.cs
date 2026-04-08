using UnityEngine;

public class Credits : MonoBehaviour
{
    Vector3 MoveCam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveCam = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraMove()
    {
       MoveCam.x = transform.position.x + 30;

    }
}
