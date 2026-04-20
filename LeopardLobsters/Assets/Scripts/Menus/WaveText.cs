using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    [SerializeField] WaveCode waveCode;

    public TextMeshProUGUI wafer;
    private void Start()
    {
        wafer.text = "Killed at wave " + waveCode.WaveNum;
    }




}