using UnityEngine;
using UnityEngine.UI;

public class TestPlacement : MonoBehaviour
{
    [SerializeField] Collider2D towerCollider;
    [SerializeField] BaseTower baseTower;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //add clamping later
        transform.position = mousePos+towerCollider.offset;

        if (Input.GetMouseButtonUp(0)) {
            if (!TestMoneyMan.self.Check(baseTower.towerCost)) { Destroy(gameObject); return; }
            Collider2D[] results = new Collider2D[1];

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Tower", "Path"));

            int overlapping = towerCollider.Overlap(filter, results); //checks if can place

            if (overlapping > 0) {
                //vfx sfx
                Destroy(gameObject);
            }

            //vfx sfx

            TestMoneyMan.self.ChangeMoney(-baseTower.towerCost);

            //neighboorhood stuff
            results = new Collider2D[100];
            filter.SetLayerMask(LayerMask.GetMask("CheckTowerPlacement"));
            filter.useTriggers = true;
            overlapping = towerCollider.Overlap(filter, results);
            foreach (Collider2D col in results) { 
                if (col == null) break;
                col.GetComponent<TowerAddedChecker>().TowerEnter(baseTower);
            }

            gameObject.layer = LayerMask.NameToLayer("Tower");
            baseTower.OnPlace.Invoke();

            Destroy(this);
        }
    }


}
