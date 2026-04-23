using UnityEngine;
using UnityEngine.UI;

public class WaveButtonCode : MonoBehaviour
{
    [SerializeField] Image button;
    [SerializeField] Color active;
    [SerializeField] Color inactive;

    void Start()
    {
        button.color = active;
        WaveCode.self.waveStarted.AddListener(WaveStarted);
        WaveCode.self.waveEnded.AddListener(WaveEnded);
    }

    void WaveStarted() {
        button.color = inactive;
    }

    void WaveEnded() {
        button.color = active;
    }
}
