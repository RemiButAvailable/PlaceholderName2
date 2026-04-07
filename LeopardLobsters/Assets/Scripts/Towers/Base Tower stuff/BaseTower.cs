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
    public String toolTip;

    [SerializeField] SpriteRenderer areaOfEffect;
    [SerializeField] SpriteRenderer[] peopleSprites = new SpriteRenderer[5];

    // Have an active and inactive sprite
    [SerializeField] SpriteRenderer towerSprite;
    [SerializeField] Color colorTint;

    [SerializeField] AudioSource PeopleAddedSound;
    [SerializeField] AudioSource PeopleRemovedSound;

    [HideInInspector] public UnityEvent<bool> isActive;
    [HideInInspector] public UnityEvent<BaseTower> Destroyed;
    [HideInInspector] public UnityEvent OnPlace; //add change sprite layer later
    [HideInInspector] public UnityEvent AddedPeople;
    [HideInInspector] public UnityEvent RemovedPeople;

    public TowerType type;


    public void AddPeople() { //connected through events

        if (people >= peopleNeeded) return;
        if (!Castle.self.personGoesOut()) return;

        if(peopleSprites[people])peopleSprites[people].enabled = true;

        people++;
        AddedPeople.Invoke();
        
        if (people >= peopleNeeded) {
           isActive.Invoke(true);
           // PeopleAddedSound.Play();
           towerSprite.color = Color.white;
        }

    }
    public bool RemovePeople() { //connected through events
        if (people <= 0) return false;
        if (people >= peopleNeeded) isActive.Invoke(false);

        people--;
        RemovedPeople.Invoke();
        Castle.self.personGoesIn();
        //PeopleRemovedSound.Play();
        towerSprite.color = colorTint;

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
    [SerializeField]TowerSelectable towerSelectable;
    public void Start()
    {
        towerSelectable.selected.AddListener(TowerSelected);
        towerSelectable.deSelected.AddListener(TowerDeselected);
        towerSprite.color = colorTint;
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
