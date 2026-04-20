using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
        BaseTower tower = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), prefab.transform.rotation);

        tower.OnPlace.AddListener(TowerPlaced);
    }
    public void BuyThing() { StartCoroutine(buyThing()); }

    [SerializeField] UnityEvent towerPlaced;

    void TowerPlaced(BaseTower tower){
        TutorialHolder.curTower = tower;
        towerPlaced.Invoke();
    }
}
