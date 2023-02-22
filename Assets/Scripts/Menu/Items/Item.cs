using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private Canvas _thisCanvas;
    private ItemScriptableObject _itemScriptable;
    private InventoryType _inventoryType;

    public ItemScriptableObject ItemScriptable { get => _itemScriptable;}

    public enum InventoryType
    {
        Character,
        Inventory
    }
    public void Set(ItemScriptableObject item, InventoryType inventoryType)
    {
        _inventoryType= inventoryType;
        if (item is CharacterItemScriptable)
        {
            _itemScriptable = item as CharacterItemScriptable;
        }
    }
    public void Use()
    {
        switch (_inventoryType)
        {
            case InventoryType.Character:
                if (UndressItem())
                {
                    Remove();
                }
                break;
            case InventoryType.Inventory:
                if (_itemScriptable is CharacterItemScriptable)
                {
                    WearItem();
                }
                break;
            default: return;

        }
    }
    private void WearItem()
    {
        FindObjectOfType<CharacterItems>().TrySetItem(_itemScriptable, this);
    }
    private bool UndressItem()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory.AddItem(_itemScriptable))
        {
            return true;
        }
        return false;
    }
    public void Select()
    {

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