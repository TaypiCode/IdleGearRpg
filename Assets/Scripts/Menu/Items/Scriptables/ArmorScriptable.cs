using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ArmorItemScriptable", menuName = "ScriptableObjects/ArmorItem", order = 1)]
public class ArmorScriptable : ItemScriptableObject
{
    [SerializeField] private float _deffence;
    [SerializeField] private ArmorPosition _armorPosition;
    public enum ArmorPosition
    {
        Head,
        Body,
        Hand,
        Leg,
        Boots
    }

    public float Deffence { get => _deffence; set => _deffence = value; }
    public ArmorPosition GetArmorPosition { get => _armorPosition;  }
}
