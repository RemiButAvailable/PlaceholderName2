using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BaseTower : MonoBehaviour
{
    public int people = 0;
    public int peopleNeeded = 2;
    public UnityEvent<bool> isActive;
    public UnityEvent<BaseTower> Destroyed;

    public int towerCost = 10;
    public int sellPrice = 5;

    
    public TowerType type;

    public void AddPeople() { //connected through events
        if (people >= peopleNeeded) return;
        if (!Castle.self.personGoesOut()) return;
        people++;
        if (people >= peopleNeeded) isActive.Invoke(true);
    }
    public bool RemovePeople(){ //connected through events
        if (people <= 0) return false;
        if (people >= peopleNeeded) isActive.Invoke(false);
        people--;
        Castle.self.personGoesIn();
        return true;
    }

    public void Sell(){ //connected through events
        MoneyManagerScript.self.changeMoney(sellPrice);
        while (RemovePeople()) ;
       //VFX SFX
       Destroy(gameObject);
    }

    /* what gets put on towers
     GetComponent<BaseTower>().isActive.AddListener(IsActive); 
    
     void IsActive(bool isTrue){
        active = isTrue;
    }
     */

    private void OnDestroy()
    {
        Destroyed.Invoke(this);
    }
}
public enum TowerType { Attack, Happy }
