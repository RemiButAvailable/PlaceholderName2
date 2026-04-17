using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BaseTower : MonoBehaviour
{
    public int people = 0;

    public int peopleNeeded = 2;
    [SerializeField] bool needsMax = true; //if needs max amuont of people to work

    public int towerCost = 10;
    public int sellPrice = 5;

    // for active and deactive feedback
    [SerializeField] SpriteRenderer areaOfEffect;
    [SerializeField] SpriteRenderer[] peopleSprites = new SpriteRenderer[5];

    [SerializeField] public SpriteRenderer towerSprite;
    [SerializeField] public Color inactiveTint;

    [SerializeField] AudioSource towerActiveSound;
    [SerializeField] AudioSource towerDeactiveSound;

    // events for tower types
    [HideInInspector] public UnityEvent<bool> isActive;
    [HideInInspector] public UnityEvent<BaseTower> Destroyed;
    [HideInInspector] public UnityEvent OnPlace; //add change sprite layer later
    [HideInInspector] public UnityEvent AddedPeople;
    [HideInInspector] public UnityEvent RemovedPeople;

    [SerializeField] AudioSource DenySound;

    public TowerType type;

    //Tower button effects
    public void AddPeople() { //acessed through button panel

        if (people >= peopleNeeded || !Castle.self.personGoesOut()) 
        { DenySound.Play(); 
         return; }
        //sound 
        //enables the sprites of the people in the tower
        if(peopleSprites[people]) 
            peopleSprites[people].enabled = true;

        people++;
        AddedPeople.Invoke();
        
        //checks if active + sfx vfx
        if (people >= peopleNeeded && needsMax) {
           isActive.Invoke(true);
           towerActiveSound?.Play();
           towerSprite.color = Color.white;
        }
    }

    public bool RemovePeople() { //acessed through button panel
        if (people <= 0) 
        {DenySound.Play();
         return false; }
        //sound
        //checks if still active
        if (people >= peopleNeeded && needsMax) { 
            isActive.Invoke(false);
            towerDeactiveSound?.Play();
            towerSprite.color = inactiveTint;
        }

        people--;
        RemovedPeople.Invoke();
        Castle.self.personGoesIn();

        if (peopleSprites[people]) peopleSprites[people].enabled = false;

        return true;
    }

    public void Sell() { //acessed through button panel
       if(WaveCode.self.WaveStart) MoneyManagerScript.self.ChangeMoney(sellPrice);
       else MoneyManagerScript.self.ChangeMoney(towerCost);
        while (RemovePeople()) ;
        Destroy(gameObject);
    }



    //tower selected stuff
    [SerializeField]TowerSelectable towerSelectable;
    public void Start()
    {
        towerSelectable.selected.AddListener(TowerSelected);
        towerSelectable.deSelected.AddListener(TowerDeselected);
        
        if(needsMax) towerSprite.color = inactiveTint;
    }
    public void TowerSelected()
    {
        if (!areaOfEffect) return;
        areaOfEffect.enabled = true;
    }
    public void TowerDeselected()
    {
        if (!areaOfEffect) return;
        areaOfEffect.enabled = false;
    }

    public void Placed() { 
        //changes sprite layer from UI 2
        towerSprite.sortingLayerID = SortingLayer.NameToID("Default");
        towerSprite.sortingOrder = 0;
        OnPlace.Invoke();
    }

    private void OnDestroy()
    {
        Destroyed.Invoke(this);
    }
}
public enum TowerType { Attack, Happy }
