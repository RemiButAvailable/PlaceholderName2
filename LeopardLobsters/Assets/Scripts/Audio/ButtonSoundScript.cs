/**********************************************************************
 * File Name: ButtonSoundScript.cs
 * 
 * Author: Dante Jones
 * Digipen Email: jones.d@digipen.edu
 * 
 * Description: This script plays a sound when
 * a button is pressed.
 * 
 * ********************************************************************/
using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{
    //The sound when the button is pressed
    public AudioSource ButtonSound;
    //Plays audio
    public void Play()
    {
        //The button sound plays
        ButtonSound.Play();
    }


}
