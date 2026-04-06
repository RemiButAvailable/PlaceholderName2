using UnityEngine;
using UnityEngine.Events;

public class TowerSelectable : MonoBehaviour
{
    public UnityEvent selected;
    public UnityEvent deSelected;

    public void Selected(){ selected.Invoke(); }
    public void Deselected() { deSelected.Invoke(); }
}
