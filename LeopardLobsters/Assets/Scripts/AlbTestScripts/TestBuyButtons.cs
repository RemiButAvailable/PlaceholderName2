using TMPro;
using UnityEngine;

public class TestBuyButtons : MonoBehaviour
{
    [SerializeField] BaseTower prefab;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI peopleText;

    private void Start()
    {
        costText.text = prefab.towerCost.ToString();
        peopleText.text = prefab.peopleNeeded.ToString();
    }

    public void buyThing() // connected by button event
    {
        Instantiate(prefab,Camera.main.ScreenToWorldPoint(Input.mousePosition), prefab.transform.rotation);
    }
}
