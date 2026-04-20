using System.Collections;
using TMPro;
using UnityEngine;

public class TestBuyButtons : MonoBehaviour
{
    [SerializeField] BaseTower prefab;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI peopleText;

    [SerializeField] float timeClickVsDrag;

    [SerializeField] AudioSource denySound;

    private void Start()
    {
        costText.text = prefab.towerCost.ToString();
        peopleText.text = prefab.peopleNeeded.ToString();
    }

    public IEnumerator buyThing() // connected by button event
    {
        if (!MoneyManagerScript.self.Check(-prefab.towerCost)) {
            denySound.Play();
            yield break;
        }

        yield return new WaitForSeconds(timeClickVsDrag);
        Instantiate(prefab,Camera.main.ScreenToWorldPoint(Input.mousePosition), prefab.transform.rotation);
    }
    public void BuyThing() { StartCoroutine(buyThing()); }
}
