using TMPro;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    [SerializeField] Animator animator;

    public void Play(string dir)
    {
        animator.Play("Number_"+dir);
    }

    public void Done() {
        Destroy(gameObject);
    }
}
