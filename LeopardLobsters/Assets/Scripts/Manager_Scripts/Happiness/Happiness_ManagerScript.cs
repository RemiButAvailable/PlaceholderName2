using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public float happiness;
    public List<Neighborhood> Neighborhoods = new List<Neighborhood>();
    
    public HappinessBar barHappyUI;
    static public Happiness_ManagerScript self;

    public AudioSource HappyGainSound;
    public AudioSource HappyLoseSound;

    private void Awake()
    {
        self = this;
    }
    void Start()
    {
        
        barHappyUI.ChangeBar(happiness);
    }

    [SerializeField] float timerMax = .1f;
    [SerializeField] float timer = 0;
    void FixedUpdate()
    {
        if(WaveCode.self.WaveStart)
        {
            timer-=Time.deltaTime;

            if (timer <= 0)
            {
                timer = timerMax + timer;

                foreach (Neighborhood hood in Neighborhoods)
                {
                    happiness += hood.curHappinessChange;
                }

                if (happiness > 1)
                {
                    happiness = 1;

                }
                if (happiness <= 0)
                {
                    //Game Lose Stuff
                    //VFX SFX
                    SceneManager.LoadScene("HappyLoseScreen");
                }

                barHappyUI.ChangeBar(happiness);
            }
        }
    }


    public void ChangeHappy(float amount)
    {
        happiness += amount;

    }
}
