using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{
    [SerializeField] Button button;
    public void Disable()
    {
        button.enabled = false;
    }
    public void Enable(){
        button.enabled = true;
    }
}
