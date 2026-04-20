using UnityEngine;

public class TowerHighlight : MonoBehaviour
{
    //highlight
    [SerializeField] GameObject gamehighlightingSprite;

    public void Highlight() { gamehighlightingSprite.SetActive(true); }
    public void DeHighlight() { gamehighlightingSprite.SetActive(false); }
}
