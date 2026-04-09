/* Author: ferg is the name ben baller did the chain*/
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    [SerializeField] Collider2D towerCollider;
    [SerializeField] GameObject towerClickCollider;
    [SerializeField] BaseTower baseTower;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //add clamping later
        transform.position = mousePos + towerCollider.offset;

        if (Input.GetMouseButtonUp(0))
        {
            if (!MoneyManagerScript.self.Check(-baseTower.towerCost)) { Destroy(gameObject); return; }

            Collider2D[] results = new Collider2D[1];
            ContactFilter2D filter = new ContactFilter2D();

            //check if placing within bounds
            filter.SetLayerMask(LayerMask.GetMask("PlaceableBounds"));
            int overlapping = towerCollider.Overlap(filter, results);
            if (overlapping <= 0){ Destroy(gameObject); return; }

            //Check if not overlaping path or towers
            filter.SetLayerMask(LayerMask.GetMask("Tower", "Path"));

            overlapping = towerCollider.Overlap(filter, results); //checks if can place

            if (overlapping > 0)
            {
                //vfx sfx
                //Fail Sound
                AudioPlayer aPlayer = Instantiate(aAudioPrefab);
                aAudioPrefab.playClip(transform.position, PlaceDenySound, SoundVolume);
                Destroy(gameObject);

                return;
            }

            

            MoneyManagerScript.self.ChangeMoney(-baseTower.towerCost);

            //neighboorhood stuff
            results = new Collider2D[100];
            filter.SetLayerMask(LayerMask.GetMask("CheckTowerPlacement"));
            filter.useTriggers = true;
            overlapping = towerCollider.Overlap(filter, results);
            foreach (Collider2D col in results)
            {
                if (col == null) break;
                col.GetComponent<TowerAddedChecker>().TowerEnter(baseTower);
            }

            //is good to place stuff

            gameObject.layer = LayerMask.NameToLayer("Tower");
            towerClickCollider.layer = LayerMask.NameToLayer("TowerSelection");
            baseTower.Placed();

            // Sound
            PlaceSound.Play();
            baseTower.TowerDeselected();
            Destroy(this);
        }
    }


}


