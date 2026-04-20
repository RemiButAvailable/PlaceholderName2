using UnityEngine;

public class ButtonSoundPlay : MonoBehaviour
{
    public AudioSource sound;

    public void play() {
        sound.Play();
    }
}
