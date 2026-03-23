using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BaseTower : MonoBehaviour
{
    int people = 0;
    public int peopleNeeded = 2;
    public UnityEvent<bool> isActive;

    public int towerCost = 10;
    public int sellPrice = 5;

    Animation buttonAppears; //animation for the buttons

    private void Start()
    {
    }

    public void AddPeople() { //connected through button
        if (people >= peopleNeeded) return;
        if (!Castle.self.personGoesOut()) return;
        people++;
        if (people >= peopleNeeded) isActive.Invoke(true);
    }
    public void RemovePeople() { //connected through button
        if (people <= 0) return;
        if (people >= peopleNeeded) isActive.Invoke(false);
        people--;
        Castle.self.personGoesIn();
    }

    public void Sell() { //connected through button
       // MoneyManagerScript.self.
       //VFX SFX
       Destroy(gameObject);
    }

    /* what gets put on towers
     GetComponent<BaseTower>().isActive.AddListener(IsActive); 
    
     void IsActive(bool isTrue){
        active = isTrue;
    }
     */
}
