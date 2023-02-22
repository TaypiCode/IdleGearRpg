using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ItemScriptable", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _countInStock;

    public int CountInStock { get => _countInStock; }
    public string ItemName { get => _itemName; }
    public Sprite Sprite { get => _sprite;}
}
