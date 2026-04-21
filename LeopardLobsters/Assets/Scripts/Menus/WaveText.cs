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