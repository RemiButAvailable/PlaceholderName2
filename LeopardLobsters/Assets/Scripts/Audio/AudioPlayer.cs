using UnityEngine;
using UnityEngine.Audio;
/*
 example of how to use

//on start
[SerializeField]AudioClip soundClip;
[SerializeField]AudioPlayer aPlayerPrefab;

//on play
AudioPlayer sound = Instantiate(aPlayerPrefab);
sound.playClip(transform.position, soundClip);

 */
public class AudioPlayer : MonoBehaviour
{
    //for playing audio after destroy

    public AudioSource player;
    bool started = false;

    public const int min = 1;
    public const int max = 100;

    //other stuff instantiate and call this for a audio source not attached to itself
    public void playClip(Vector3 position, AudioClip clip, float volume = 1, int top = max, int bot = min)
    {
        transform.position = position;

        player.clip = clip;
        player.volume = volume;

        player.maxDistance = max;
        player.minDistance = min;

        player.Play();
        started = true;
    }
    public void playClip(Vector3 position, AudioResource clip, float volume = 1, int top = max, int bot = min)
    {
        transform.position = position;

        player.resource = clip;
        player.volume = volume;

        player.maxDistance = max;
        player.minDistance = min;

        player.Play();
        started = true;
    }

    //destroys iteself when done.
    public void Update()
    {
        if (started && !player.isPlaying) Destroy(gameObject);
    }
}
