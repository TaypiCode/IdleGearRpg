using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ItemScriptable", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private bool _usable;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _countInStock;
    [SerializeField] private int _startGrade;
    [SerializeField] private int _maxGrade;

    public int CountInStock { get => _countInStock; }
    public string ItemName { get => _itemName; }
    public Sprite Sprite { get => _sprite;}
    public int StartGrade { get => _startGrade;  }
    public int MaxGrade { get => _maxGrade;  }
    public bool Usable { get => _usable; }
}
