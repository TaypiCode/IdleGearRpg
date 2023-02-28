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
    [SerializeField] private Image _itemGradeImage;
    [SerializeField] private GameObject _useBtn;
    [SerializeField] private GameObject _deleteBtn;
    private ItemScriptableObject _itemScriptable;
    private InventoryType _inventoryType;
    private int _itemsCount;
    private int _itemGrade;
    private int _upgradeXP;
    private bool _inInventory;
    public ItemScriptableObject ItemScriptable { get => _itemScriptable;}
    public int ItemsCount { get => _itemsCount;  }
    public int ItemGrade { get => _itemGrade;  }
    public int UpgradeXP { get => _upgradeXP; }

    public enum InventoryType
    {
        Character,
        Inventory,
        Upgrade
    }
    public void Set(ItemScriptableObject item, InventoryType inventoryType, int itemGrade = 1, int xp = 0, int itemsCount = 1, bool inInventory = true)
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
        else if (item is UpgradeItemScriptable)
        {
            _itemScriptable = item as UpgradeItemScriptable;
        }
        _itemsCount = itemsCount;
        _itemImage.sprite = item.Sprite;
        _upgradeXP = xp;
        TryUpdateGrade(itemGrade);
        if (itemsCount > _itemScriptable.CountInStock)
        {
            _itemsCount = _itemScriptable.CountInStock;
        }
        _inInventory = inInventory;
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
            case InventoryType.Upgrade:
                if (UndressItem())
                {
                    Remove();
                    FindObjectOfType<UpgradeItems>().UpdateAfterUndress();
                }
                UnSelect();
                break;
            case InventoryType.Inventory:
                if(_itemScriptable is UpgradeItemScriptable)
                {
                    if (_itemScriptable is CharacterItemScriptable)
                    {
                        if (FindObjectOfType<CharacterItems>().IsActive())
                        {
                            WearItem();
                        }
                        else if (FindObjectOfType<UpgradeItems>().IsActive())
                        {
                            SendItemToUpgrade();
                        }
                    }
                    else
                    {
                        SendItemToUpgrade();
                    }
                }
                UnSelect();
                break;
            default: return;

        }
    }
    private void SendItemToUpgrade()
    {
        if (FindObjectOfType<UpgradeItems>().TrySetItem(this))
        {
            Remove();
        }
    }
    private void WearItem()
    {

        FindObjectOfType<CharacterItems>().TrySetItem(_itemScriptable, this, _itemGrade, _upgradeXP);
    }
    private bool UndressItem()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory.CreateItem(_itemScriptable,_itemGrade, _upgradeXP, _itemsCount))
        {
            return true;
        }
        return false;
    }
    public void Select()
    {
        if (_inInventory)
        {
            FindObjectOfType<ItemOverview>().ShowItemOverview(this);
            _useBtn.SetActive(_itemScriptable.Usable);
            _deleteBtn.SetActive(true);
        }
    }
    public void UnSelect()
    {
        if (_inInventory)
        {
            FindObjectOfType<ItemOverview>().HideItemOverview();
            _useBtn.SetActive(false);
            _deleteBtn.SetActive(false);
        }
    }
    public void Remove()
    {
        UnSelect();
        GetComponentInParent<InventoryCell>().DeleteItem(true);
    }
    public void TryUpdateGrade(int grade)
    {
        if(grade > _itemScriptable.MaxGrade)
        {
            Debug.Log("Out of max grade");
            return;
        }
        _itemGrade = grade;
        _itemGradeImage.color = ItemGradeColor.GetGradeColor(_itemGrade);
    }
    public void ChangeCount(int val)
    {
        _itemsCount += val;
        if(_itemsCount <= 0)
        {
            Remove();
        }
    }
}
[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}