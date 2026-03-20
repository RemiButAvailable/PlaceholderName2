using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MoneyManagerScript : MonoBehaviour
{
    public int moneyNum;
    GameObject spawnedProduct;
    Vector3 mousePos;
    public List<GameObject> products;
    bool DragnDrop;
    GameObject selectedProduct;
    public TextMeshProUGUI textMoney;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMoney.text = moneyNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < products.Count; i++)
            {
                if(Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), products[i].transform.position) <= 0.1f)
                {
                    Buy(products[i]);
                    break;
                }
            }
        }
        if(DragnDrop && Input.GetMouseButton(0))
        {
            selectedProduct.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Input.GetMouseButtonUp(0))
            {
                DragnDrop = false;
                //spawnedProduct = Instantiate(selectedProduct.transform.position);
            }
        }
    }
    public void Buy(GameObject product)
    {
        if(moneyNum >= product.GetComponent<ProductScript>().price)
        {
            changeMoney(-1);
            DragnDrop = true;
            selectedProduct = product;
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
