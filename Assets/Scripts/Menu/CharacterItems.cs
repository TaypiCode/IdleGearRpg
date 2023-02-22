using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterItems : MonoBehaviour
{
    [SerializeField] private InventoryCell _headItemCell;
    [SerializeField] private InventoryCell _bodyItemCell;
    [SerializeField] private InventoryCell _handItemCell;
    [SerializeField] private InventoryCell _legItemCell;
    [SerializeField] private InventoryCell _bootsItemCell;
    [SerializeField] private InventoryCell _cloakItemCell;
    [SerializeField] private InventoryCell _neckItemCell;
    [SerializeField] private InventoryCell _earringItemCell;
    [SerializeField] private InventoryCell _ringItemCell;
    [SerializeField] private InventoryCell _mainHandItemCell;
    [SerializeField] private InventoryCell _secondHandItemCell;
    [SerializeField] private InventoryCell _musicianItemCell;
    private ArmorScriptable _headArmorData;
    public void TrySetArmor(ArmorItem item)
    {
        ArmorScriptable scriptable = item.GetArmorScriptable;
        switch(scriptable.GetArmorPosition)
        {
            case ArmorScriptable.ArmorPosition.Head:
                _headItemCell.SetItem(scriptable);
                item.ItemParent.Remove();
                _headArmorData = scriptable;
                break;
            default: return;
        }
        
        CalculateStats();
    }
    private void CalculateStats()
    {
        PlayerData.playerDeffence += _headArmorData.Deffence;
    }
}
