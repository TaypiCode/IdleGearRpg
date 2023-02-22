using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Canvas _thisCanvas;
    [SerializeField] private Image _itemImage;
    [SerializeField] private GameObject _useBtn;
    [SerializeField] private GameObject _deleteBtn;
    private ItemScriptableObject _itemScriptable;
    private InventoryType _inventoryType;
    private int _itemsCount;
    public ItemScriptableObject ItemScriptable { get => _itemScriptable;}
    public int ItemsCount { get => _itemsCount;  }

    public enum InventoryType
    {
        Character,
        Inventory
    }
    public void Set(ItemScriptableObject item, InventoryType inventoryType, int itemsCount = 1)
    {
        _inventoryType= inventoryType;
        if (item is CharacterItemScriptable)
        {
            _itemScriptable = item as CharacterItemScriptable;
        }
        else if (item is MiscItemScriptable)
        {
            _itemScriptable = item as MiscItemScriptable;
        }
        _itemImage.sprite = item.Sprite;
        _itemsCount = itemsCount;
        if (itemsCount > _itemScriptable.CountInStock)
        {
            _itemsCount = _itemScriptable.CountInStock;
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
                UnSelect();
                break;
            case InventoryType.Inventory:
                if (_itemScriptable is CharacterItemScriptable)
                {
                    WearItem();
                }
                UnSelect();
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
        if (inventory.AddItem(_itemScriptable, 1))
        {
            return true;
        }
        return false;
    }
    public void Select()
    {
        FindObjectOfType<Inventory>().ShowItemOverview(this);
        _useBtn.SetActive(true);
        _deleteBtn.SetActive(true);
    }
    public void UnSelect()
    {
        FindObjectOfType<Inventory>().HideItemOverview();
        _useBtn.SetActive(false);
        _deleteBtn.SetActive(false);
    }
    public void Remove()
    {
        UnSelect();
        GetComponentInParent<InventoryCell>().DeleteItem(true);
    }

}
[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}