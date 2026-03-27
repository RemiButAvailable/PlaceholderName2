using System.Collections;
using TMPro;
using UnityEngine;

public class TestBuyButtons : MonoBehaviour
{
    [SerializeField] BaseTower prefab;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI peopleText;
    [SerializeField] float timeClickVsDrag;
    private void Start()
    {
        costText.text = prefab.towerCost.ToString();
        peopleText.text = prefab.peopleNeeded.ToString();
    }

    public IEnumerator buyThing() // connected by button event
    {
        yield return new WaitForSeconds(timeClickVsDrag);
        Instantiate(prefab,Camera.main.ScreenToWorldPoint(Input.mousePosition), prefab.transform.rotation);
    }
    public void BuyThing() { StartCoroutine(buyThing()); }
}
