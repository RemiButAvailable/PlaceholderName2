using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MoneyManagerScript : MonoBehaviour
{
    public int moneyNum;
    GameObject spawnedProduct;
    GameObject spawnedBuilding;
    Vector3 mousePos;
    public List<GameObject> products;
    bool DragnDrop;
    public TextMeshProUGUI textMoney;
    public List<GameObject> buildings;
    public GameObject happinessManager;
    Happiness_ManagerScript happiness_ManagerScript;
    Collider2D[] overlappingObjs;
    Collider2D[] overlappingGAs;
    ContactFilter2D contactFilter;
    ContactFilter2D contactFilterGA;

    static public MoneyManagerScript self;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        happiness_ManagerScript = happinessManager.GetComponent<Happiness_ManagerScript>();
        textMoney.text = moneyNum.ToString();
        self = this;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < products.Count; i++)
            {
                if(Vector3.Distance(mousePos, products[i].transform.position) <= 1)
                {
                    Buy(products[i]);
                    break;
                }
            }
        }
        if(DragnDrop && Input.GetMouseButton(0))
        {
            spawnedProduct.transform.position = mousePos;
        }
        if (DragnDrop && Input.GetMouseButtonUp(0))
        {
            int excludedLayerOne = LayerMask.NameToLayer("background");
            int excludedLayerTwo = LayerMask.NameToLayer("neighborhoodGreaterAreas");
            int mask = ~((1 << excludedLayerOne) | (1 << excludedLayerTwo));
            overlappingObjs = new Collider2D[1];
            contactFilter = new ContactFilter2D();
            contactFilter.useLayerMask = true;
            contactFilter.SetLayerMask(mask);
            int count = spawnedProduct.GetComponent<Collider2D>().Overlap(contactFilter, overlappingObjs);
            if (count > 1)
            {
                //play not enough money sound
                Destroy(gameObject);
                DragnDrop = false;
            }
            else
            {
            spawnedBuilding = Instantiate(buildings[spawnedProduct.GetComponent<ProductScript>().ID], spawnedProduct.transform.position, Quaternion.identity);

                //play place tower sound

                Destroy(spawnedProduct);
                DragnDrop = false;
                //happiness_ManagerScript.CalculateHappiness(spawnedBuilding);
                int greaterAreasLayer = LayerMask.NameToLayer("neighborhoodGreaterAreas");
                int galMask = 1 << greaterAreasLayer;
                overlappingGAs = new Collider2D[3];
                contactFilterGA = new ContactFilter2D();
                contactFilterGA.useLayerMask = true;
                contactFilterGA.SetLayerMask(galMask);
                int countGA = spawnedProduct.GetComponent<Collider2D>().Overlap(contactFilterGA, overlappingGAs);
                
                /*for(int i = 0; i < countGA; i++)
                {
                    overlappingGAs[i].gameObject.GetComponent<NeighborhoodScript>().Towe
                }*/
            }
        }
    }
    public void Buy(GameObject product)
    {
        if(moneyNum >= product.GetComponent<ProductScript>().price)
        {
            changeMoney(-1);
            DragnDrop = true;
            spawnedProduct = Instantiate(product, mousePos, Quaternion.identity);

            //play buy sound
        }
        else
        {
            //play not enough money sound, price turns red
        }
    }

    public void changeMoney(int num) {
        moneyNum += num;
        textMoney.text = moneyNum.ToString();
    }
}
