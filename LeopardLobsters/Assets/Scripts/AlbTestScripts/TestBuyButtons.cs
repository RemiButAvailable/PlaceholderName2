using UnityEngine;

public class TestBuyButtons : MonoBehaviour
{
    [SerializeField] TestPlacement prefab;

    public void buyThing()
    {
        Instantiate(prefab);
    }
}
