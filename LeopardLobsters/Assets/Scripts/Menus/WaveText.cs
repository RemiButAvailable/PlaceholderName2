using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    [SerializeField] WaveCode waveCode;

    // Wafer :3
    public TextMeshProUGUI wafer;
    private void Start()
    {
        wafer.text = "Killed at wave " + waveCode.WaveNum;
    }




}