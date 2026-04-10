using TMPro;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    [SerializeField] ButtonPanel buttonPanel;
    //[SerializeField] Animation buttonPanelAnimation;
    TowerSelectable towerSelected;
    [SerializeField]TextMeshProUGUI toolTips;
    TowerHighlight towerHighlighed;

    private void Update()
    {
        //Tower highlight and tool tips stuff
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, LayerMask.GetMask("TowerSelection", "Button")); //change later to be editable in inspector or somehting

        if (hit.collider)
        {
            TowerToolTip tips = hit.collider.GetComponent<TowerToolTip>();
            if (tips)
            {
                toolTips.text = tips.toolTip;
            }

            //highlights stuff when hovering over it
            TowerHighlight highlighted = hit.collider.GetComponent<TowerHighlight>();
            if (highlighted != towerHighlighed) { towerHighlighed?.DeHighlight(); }
            towerHighlighed = highlighted;
            towerHighlighed?.Highlight();
        }
        else {
            towerHighlighed?.DeHighlight();
        }

        // Tower selection stuff
        if (Input.GetMouseButtonDown(0)) {
            if (hit.collider && hit.collider.gameObject.layer == LayerMask.NameToLayer("TowerSelection"))
            {
                towerSelected?.Deselected();
                towerSelected  =hit.collider.GetComponentInParent<TowerSelectable>();
                towerSelected?.Selected();


                if (towerSelected.gameObject.tag == "Tower")
                {
                    buttonPanel.transform.position = hit.collider.transform.position;
                    //ButtonPanelAnimation.Play("Clicked"); //maybe later
                    buttonPanel.towerSelect(hit.collider.GetComponentInParent<BaseTower>());
                    buttonPanel.gameObject.SetActive(true);
                }
            }
            
            else if (!hit.collider && hit.collider?.gameObject.layer != LayerMask.NameToLayer("Button"))
            {
                towerSelected?.Deselected();

                //ButtonPanelAnimation.Play();
                buttonPanel.gameObject.SetActive(false);
            }
        }
    }
}
