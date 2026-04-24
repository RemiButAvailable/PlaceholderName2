using System;
using UnityEngine;
using UnityEngine.Events;

public class TutorialDisable: MonoBehaviour
{
    [SerializeField] UnityEvent disable;
    [SerializeField] UnityEvent enable;
    public void Disable() { disable.Invoke(); }
    public void Enable() { enable.Invoke(); }
}
