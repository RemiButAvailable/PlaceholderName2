using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AnimationHolder : MonoBehaviour
{
    [SerializeField] NumberScript prefab;
    [SerializeField] Transform numAnimatorParent;
    public List<int> hold = new List<int>();
    [SerializeField] float cooldown = .1f;
    float timer = 0;
    [SerializeField] String textAfter;

    enum Dir {Up = 1,Down = 2,Left = 3,Right = 4}
    String[] Dirs = { "Up", "Down", "Left", "Right" };
    [SerializeField] Dir direction = Dir.Down;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else {
            if (hold.Count > 0)
            {
                timer = cooldown;

                int num = hold[0];
                hold.RemoveAt(0);

                NumberScript ani = Instantiate(prefab, numAnimatorParent);

                string text = num.ToString() + textAfter;
                if (num > 0) text = "+" + text;

                ani.text.text = text;
                ani.Play(Dirs[(int)direction-1]);
            }
        }
    }

    public void Add(int n) { hold.Add(n); }
}
