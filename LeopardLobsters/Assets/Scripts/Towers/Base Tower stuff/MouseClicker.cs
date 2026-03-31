using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    [SerializeField] ButtonPanel buttonPanel;
    //[SerializeField] Animation buttonPanelAnimation;
    BaseTower towerSelected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, LayerMask.GetMask("TowerSelection", "Button")); //change later to be editable in inspector or somehting
            
            if (hit.collider && hit.collider.gameObject.layer == LayerMask.NameToLayer("TowerSelection"))
            {
                towerSelected?.TowerDeselected();
                towerSelected  =hit.collider.GetComponentInParent<BaseTower>();
                towerSelected?.TowerSelected();

                buttonPanel.transform.position = hit.collider.transform.position;
                //ButtonPanelAnimation.Play("Clicked"); //maybe later
                buttonPanel.towerSelect(hit.collider.GetComponentInParent<BaseTower>());
                buttonPanel.gameObject.SetActive(true);
            }
            else if (!hit.collider && hit.collider?.gameObject.layer != LayerMask.NameToLayer("Button"))
            {
                towerSelected?.TowerDeselected();

                //ButtonPanelAnimation.Play();
                buttonPanel.gameObject.SetActive(false);
            }
        }
    }
}
