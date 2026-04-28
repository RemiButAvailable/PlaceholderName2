using UnityEngine;
using UnityEngine.Events;

public class TowerHighlight : MonoBehaviour
{
    //highlight
    [SerializeField] GameObject gamehighlightingSprite;
    public UnityEvent Highlighted;
    public UnityEvent DeHighlighted;
    bool highlighted;

    public void Highlight() { 
        gamehighlightingSprite.SetActive(true);
        Highlighted.Invoke();
    }
    public void DeHighlight() { 
        gamehighlightingSprite.SetActive(false);
        DeHighlighted.Invoke();
    }
}
