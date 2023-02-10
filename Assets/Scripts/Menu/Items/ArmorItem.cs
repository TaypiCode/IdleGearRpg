using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : Item, IItem
{
    [SerializeField] private ArmorScriptable _armorScriptable;

    

    private void Start()
    {
        if(_armorScriptable == null)
        {
            
        }
    }
    public void Select()
    {

    }

    public void Use()
    {

    }
}
