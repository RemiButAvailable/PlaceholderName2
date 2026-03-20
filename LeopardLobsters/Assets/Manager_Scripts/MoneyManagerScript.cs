using UnityEngine;
using System.Collections.Generic;

public class MoneyManagerScript : MonoBehaviour
{
    public int moneyNum;
    GameObject spawnedBuilding;
    GameObject spawnedProduct;
    Vector3 mousePos;
    public List<GameObject> products;
    public List<GameObject> buildingTypes;
    bool DragnDrop;
    GameObject selectedProduct;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < products.Count; i++)
            {
                Debug.Log("distance is " + Vector3.Distance(mousePos, products[i].transform.position));
                if(Vector3.Distance(mousePos, products[i].transform.position) <= 1)
                {
                    Buy(products[i]);
                    break;
                }
            }
        }
        if(DragnDrop && Input.GetMouseButton(0))
        {
            selectedProduct.transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0) && DragnDrop)
        {
            DragnDrop = false;
            spawnedBuilding = Instantiate(buildingTypes[selectedProduct.GetComponent<ProductScript>().ID], selectedProduct.transform.position, Quaternion.identity);
            Destroy(selectedProduct);
        }
    }
    public void Buy(GameObject product)
    {
        Debug.Log("bought product");
        if (moneyNum >= product.GetComponent<ProductScript>().price)
        {
            moneyNum -= 1;
            DragnDrop = true;
            selectedProduct = product;
        }
        else
        {
            //play not enough money sound, price turns red
        }
    }
}
