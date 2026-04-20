/* Author: ferg is the name ben baller did the chain*/
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Placement : MonoBehaviour //places tower, created when pressing buy button, is put on the tower prefabs
{
    [SerializeField] Collider2D towerCollider;
    [SerializeField] GameObject towerClickCollider;
    [SerializeField] BaseTower baseTower;

    //Colors n stuff
    [SerializeField] Color denyTint;

    [Space] //sound stuff
    [SerializeField] AudioSource PlaceSound;
   
    [SerializeField] AudioResource PlaceDenySound;
    [SerializeField] AudioPlayer aAudioPrefab;
    [SerializeField] float SoundVolume = .5f;


    private void Start()
    {
        baseTower.TowerSelected();
    }

    private void Update()
    {
        //moving tower to mouse pos
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePos + towerCollider.offset;


        //checking if is placeable spots
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[1];
        bool goodSpot = true;

        //check if placing within bounds
        filter.SetLayerMask(LayerMask.GetMask("PlaceableBounds"));
        int inBounds = towerCollider.Overlap(filter, results);

        //Check if not overlaping path or towers
        filter.SetLayerMask(LayerMask.GetMask("Tower", "Path"));
        int touchingPath = towerCollider.Overlap(filter, results);


        if (inBounds <= 0 || touchingPath > 0) { 
            baseTower.towerSprite.color = denyTint; 
            goodSpot = false;
        }
        else { 
            baseTower.towerSprite.color = baseTower.inactiveTint; 
        }


        if (Input.GetMouseButtonUp(0))
        {
            //prob doesnt need to check cus button already checks but whatever
            if (!MoneyManagerScript.self.Check(-baseTower.towerCost)) { //checks if has enough money
                Destroy(gameObject); 
                return; 
            }

            if (!goodSpot)
            {
                //Fail Sound
                AudioPlayer aPlayer = Instantiate(aAudioPrefab);
                aAudioPrefab.playClip(transform.position, PlaceDenySound, SoundVolume);

                Destroy(gameObject);
                return;
            }

            //tower is good to place from now on
            MoneyManagerScript.self.ChangeMoney(-baseTower.towerCost);

            //checks neighboorhoods and fountains
            results = new Collider2D[100];
            filter.SetLayerMask(LayerMask.GetMask("CheckTowerPlacement"));
            filter.useTriggers = true;

            inBounds = towerCollider.Overlap(filter, results);
            
            foreach (Collider2D col in results)
            {
                if (col == null) break;
                col.GetComponent<TowerAddedChecker>().TowerEnter(baseTower);
            }

            //changing layers out of ignoreRaycast
            gameObject.layer = LayerMask.NameToLayer("Tower");
            towerClickCollider.layer = LayerMask.NameToLayer("TowerSelection");
            baseTower.Placed();

            // Sound
            PlaceSound.Play();
            baseTower.TowerDeselected();

            Destroy(this); //removes this script
        }
    }


}


