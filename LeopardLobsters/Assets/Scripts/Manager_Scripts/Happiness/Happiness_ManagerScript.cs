/*
 * Description: Will manage the happiness, as in keeping track of things related to it,
 * Adding it, removing it, having weird fluctuations, stuff like that.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Happiness_ManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public float happiness;
    public List<Neighborhood> Neighborhoods = new List<Neighborhood>();
    [HideInInspector]public List<TestFountain> Fountains = new List<TestFountain>();
    
    public HappinessBar barHappyUI;
    static public Happiness_ManagerScript self;

    public AudioSource HappyGainSound;
    public AudioSource HappyLoseSound;

    public UnityEvent towerPlaced;
    public UnityEvent towerRemoved; //move later to a tower manager for tutorial and other stuff in future

    private void Awake()
    {
        self = this;
    }
    void Start()
    {
        barHappyUI.ChangeBar(happiness);
        towerPlaced.AddListener(happyChangeChanged);
        towerRemoved.AddListener(happyChangeChanged);
        happyChangeChanged();
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

    public void FountainAdd(TestFountain fountain) {
        Fountains.Add(fountain);
        happyChangeChanged();
    }
    public void FountainRemove(TestFountain fountain)
    {
        Fountains.Remove(fountain);
        happyChangeChanged();
    }

    [Space]
    [Space]
    [SerializeField]
    Image happyIcon;
    [SerializeField]
    SpriteFloat[] happinessChangeIndicator;
    int index = 0;

    void happyChangeChanged() { //this is also kinda jank but idk
        
        float curHappyChange = 0;
        foreach (TestFountain fountain in Fountains) {
            curHappyChange += fountain.getHappinessWithTime(timerMax);
        }
        foreach (Neighborhood neigh in Neighborhoods) {
            curHappyChange += neigh.curHappinessChange;
        }
        GetIndex(curHappyChange, happinessChangeIndicator);
        happyIcon.sprite = happinessChangeIndicator[index].sRenderer;
    }

    
    void GetIndex(float percent, SpriteFloat[] list)
    {
        if (percent > list[index].num)
        {
            if (index + 1 >= list.Length) { index = list.Length - 1; return; }
            if (percent < list[index + 1].num) { return; }
            index++;
            GetIndex(percent,list);
        }
        else if (percent < list[index].num)
        {
            if (index - 1 < 0) { index = 0; return; }
            index--;
            GetIndex(percent,list);
        }
    }

    [Serializable]
    class SpriteFloat: IComparable<SpriteFloat> { //replace with float color later
        [SerializeField]public Sprite sRenderer;
        [SerializeField]public float num;

        public int CompareTo(SpriteFloat other) {
            if (other == null) return 1;
            return this.num.CompareTo(other.num);
        }
    }
}
