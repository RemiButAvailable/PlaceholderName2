using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BaseTower : MonoBehaviour
{
    public int people = 0;
    public int peopleNeeded = 2;
    public int towerCost = 10;
    public int sellPrice = 5;

    [SerializeField] SpriteRenderer areaOfEffect;
    [SerializeField] SpriteRenderer[] peopleSprites = new SpriteRenderer[5];


    public UnityEvent<bool> isActive;
    public UnityEvent<BaseTower> Destroyed;
    public UnityEvent OnPlace; //add change sprite layer later
    public UnityEvent AddedPeople;
    public UnityEvent RemovedPeople;

    public TowerType type;

    public void AddPeople() { //connected through events
        while (people < peopleNeeded)
        {
            // Have the grayed out tower, or just different color to show inactive.

        }

        if (people >= peopleNeeded) return;
        if (!Castle.self.personGoesOut()) return;

        if(peopleSprites[people])peopleSprites[people].enabled = true;

        people++;
        AddedPeople.Invoke();

        if (people >= peopleNeeded) isActive.Invoke(true);

    }
    public bool RemovePeople() { //connected through events
        if (people <= 0) return false;
        if (people >= peopleNeeded) isActive.Invoke(false);

        people--;
        RemovedPeople.Invoke();
        Castle.self.personGoesIn();

        if (peopleSprites[people]) peopleSprites[people].enabled = false;

        return true;
    }

    public void Sell() { //connected through events
        //MoneyManagerScript.self.changeMoney(sellPrice);
        MoneyManagerScript.self.ChangeMoney(sellPrice);
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

    private void OnDestroy()
    {
        Destroyed.Invoke(this);
    }

    public Vector3 GetClosestPointOnCollider(Collider2D neighborhoodCollider)
    {
        Vector3 closestPointOnCollider = neighborhoodCollider.GetComponent<Collider2D>().ClosestPoint(transform.position);
        return closestPointOnCollider;
    }
}
public enum TowerType { Attack, Happy }
