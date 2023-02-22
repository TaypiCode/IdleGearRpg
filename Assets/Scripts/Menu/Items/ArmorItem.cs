using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : MonoBehaviour, IItem
{
    private ArmorScriptable _armorScriptable;
    private Item _itemParent;

    public ArmorScriptable GetArmorScriptable { get => _armorScriptable;}
    public Item ItemParent { get => _itemParent;  }

    public void Set(ItemScriptableObject armor, Item parent)
    {
        _armorScriptable = armor as ArmorScriptable;
        _itemParent = parent;
        _itemParent.OnUseEvent?.AddListener(Use);
        _itemParent.OnSelectEvent?.AddListener(Select);
    }
    public void Select()
    {
        print("select");
    }
    public void Use()
    {
        print("use");
        FindObjectOfType<CharacterItems>().TrySetArmor(this);
    }
}
