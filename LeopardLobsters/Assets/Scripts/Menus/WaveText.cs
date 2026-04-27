/**********
 * Forgor to document what actual day so 4/27/26
 * Author: Victoria T.
 * 
 * 
 * Description: Just something to document at what wave the player died, in case
 * they wanted to know.
 */
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{

    // Waffer :3
    public TextMeshProUGUI waffer;
    private void Start()
    {
        waffer.text = "Killed at wave " + WaveCode.WaveNum;
    }




}