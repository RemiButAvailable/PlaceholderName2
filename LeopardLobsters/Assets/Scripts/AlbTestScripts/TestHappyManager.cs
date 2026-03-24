using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestHappyManager : MonoBehaviour
{
    float happy = 100;
    [SerializeField]HappinessBar bar;
    public static TestHappyManager self;

    private void Start()
    {
        self = this;
    }


    public void ChangeHappy(float change) {
        happy += change;
        bar.ChangeBar(happy);

        if (happy <= 0) {
            //vfx sfx
            SceneManager.LoadScene("HappyLoseScreen");
        }
        if (happy > 100) happy = 100;
    }


}
