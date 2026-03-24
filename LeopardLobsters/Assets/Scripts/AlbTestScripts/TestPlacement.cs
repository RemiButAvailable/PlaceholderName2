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
            Collider2D[] results = new Collider2D[1];

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Tower", "Path"));
            filter.useLayerMask = true;

            int noTowerHit = towerCollider.Overlap(filter, results); //checks if can place

            if (noTowerHit > 0) {
                //vfx sfx
                Destroy(gameObject);
            }

            //vfx sfx

            //MoneyManager.changeMoney(baseTower.towerCost);

            //neighboorhood stuff
            RaycastHit2D neighborHit = Physics2D.Raycast(towerCollider.offset+(Vector2)transform.position,
                Vector2.zero, 1f, LayerMask.GetMask("Neighborhood")); // checks if has neighboorhood, can change to be collider cast
            if (neighborHit) { 
                neighborHit.collider.GetComponent<TestNeighborhood>().towerEnter(baseTower); 
            }

            gameObject.layer = LayerMask.NameToLayer("Tower");
            Destroy(this);
        }
    }


}
