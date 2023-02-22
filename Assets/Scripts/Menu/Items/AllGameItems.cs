using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGameItems : MonoBehaviour
{
    [SerializeField] private List<ItemScriptableObject> _items = new List<ItemScriptableObject>();

    public List<ItemScriptableObject> Items { get => _items; }
}
