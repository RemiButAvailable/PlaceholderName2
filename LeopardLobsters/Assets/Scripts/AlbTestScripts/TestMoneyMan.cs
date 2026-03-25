using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TestMoneyMan : MonoBehaviour
{
    public int money = 10;
    public TextMeshProUGUI text;
    public static TestMoneyMan self;
    private void Awake()
    {
        self = this;
        text.text = money.ToString();
    }
    public void ChangeMoney(int i) { money += i; text.text = money.ToString(); }

    public bool Check(int price)
    {
        if (price <= money) return true;
        return false;

    }
}
