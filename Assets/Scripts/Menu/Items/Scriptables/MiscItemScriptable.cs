using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MiscScriptable", menuName = "ScriptableObjects/MiscItem", order = 1)]
public class MiscItemScriptable : ItemScriptableObject
{
    [SerializeField] private string _info;

    public string Info { get => _info;  }
}
