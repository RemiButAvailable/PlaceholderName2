using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    [SerializeField] ButtonPanel buttonPanel;
    //[SerializeField] Animation buttonPanelAnimation;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, ~LayerMask.GetMask("Ignore Raycast"));
            if (hit.collider && hit.collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                buttonPanel.transform.position = hit.collider.transform.position;
                //ButtonPanelAnimation.Play("Clicked"); //maybe later
                buttonPanel.towerSelect(hit.collider.GetComponent<BaseTower>());
                buttonPanel.gameObject.SetActive(true);
            }
            else if (!hit.collider && hit.collider?.gameObject.layer != LayerMask.NameToLayer("Button"))
            {
                //ButtonPanelAnimation.Play();
                buttonPanel.gameObject.SetActive(false);
            }
        }
    }
}
