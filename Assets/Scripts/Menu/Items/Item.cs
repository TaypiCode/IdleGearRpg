using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField]private Canvas _thisCanvas;
    public UnityEvent OnUseEvent = new UnityEvent();
    public UnityEvent OnSelectEvent = new UnityEvent();
    public void Use()
    {
        OnUseEvent?.Invoke();
    }
    public void Select()
    {
        OnSelectEvent?.Invoke();
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}