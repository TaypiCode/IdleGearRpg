using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AllGameItems _allGameItems;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();
    private List<ItemScriptableObject> _playerItems = new List<ItemScriptableObject>();
    private List<int> _playerItemsCount = new List<int>();
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
}
