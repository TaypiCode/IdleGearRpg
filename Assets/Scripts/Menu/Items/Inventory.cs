using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AllGameItems _allGameItems;
    [Header("Overview Item")]
    [SerializeField] private GameObject _itemOverviewCanvas;
    [SerializeField] private Image _itemOverviewImage;
    [SerializeField] private TextMeshProUGUI _itemOverviewText;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();
    private List<ItemScriptableObject> _playerItems = new List<ItemScriptableObject>(); //need fill from save
    private List<int> _playerItemsCount = new List<int>();//need fill from save
    private Item _selectedItem;
    private void Start()
    {
        for (int i = 0; i < _allGameItems.Items.Count; i++) //for test
        {
            AddItem(_allGameItems.Items[i], 1);
        }
        for (int i = 0; i < _playerItems.Count; i++) 
        {
            AddItem(_playerItems[i], _playerItemsCount[i]);
        }
    }
    public bool AddItem(ItemScriptableObject item, int count)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (count > 0)
            {
                if (_cells[i].GetItem != null)
                {
                    if (_cells[i].GetItem.ItemScriptable.itemId == item.itemId)
                    {
                        if (_cells[i].GetItem.ItemsCount < item.CountInStock)
                        {
                            if (_cells[i].GetItem.ItemsCount + count <= item.CountInStock)
                            {
                                _cells[i].SetItem(item, Item.InventoryType.Inventory, _cells[i].GetItem.ItemsCount + count);
                                count -= count;
                                if (count == 0)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                int added = item.CountInStock - _cells[i].GetItem.ItemsCount;
                                count -= added;
                                _cells[i].SetItem(item, Item.InventoryType.Inventory, item.CountInStock);
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < _cells.Count; i++)
        {
            if (count > 0)
            {
                if (_cells[i].GetItem == null)
                {
                    _cells[i].SetItem(item, Item.InventoryType.Inventory, count);
                    count -= count;
                    if (count == 0)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void ShowItemOverview(Item item)
    {
        string newString = "\n";
        _itemOverviewText.text = item.ItemScriptable.ItemName + newString;
        _itemOverviewImage.sprite = item.ItemScriptable.Sprite;
        if (item.ItemScriptable is CharacterItemScriptable)
        {
            CharacterItemScriptable data = item.ItemScriptable as CharacterItemScriptable;
            if (data.Hp != 0)
            {
                _itemOverviewText.text += "Здоровье: " + Mathf.RoundToInt(data.Hp) + newString;
            }
            if (data.Damage != 0)
            {
                _itemOverviewText.text += "Урон: " + StringConverter.ConvertToFormat(data.Damage) + newString;
            }
            if (data.Deffence != 0)
            {
                _itemOverviewText.text += "Защита: " + StringConverter.ConvertToFormat(data.Deffence) + "%" + newString;
            }
            if (data.AttackSpeed != 0)
            {
                _itemOverviewText.text += "Скорость атаки: " + StringConverter.ConvertToFormat(data.AttackSpeed)+newString;
            }
            
        }
        else if(item.ItemScriptable is MiscItemScriptable){
            MiscItemScriptable data = item.ItemScriptable as MiscItemScriptable;
            _itemOverviewText.text += data.Info;
        }
        _itemOverviewText.text += "Количество: " + item.ItemsCount;
        _itemOverviewCanvas.SetActive(true);
    }
    public void HideItemOverview()
    {
        _itemOverviewCanvas.SetActive(false);
    }
}
