using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AllGameItems _allGameItems;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();
    private List<ItemScriptableObject> _playerItems = new List<ItemScriptableObject>();
    private void Start()
    {
        for (int i = 0; i < _allGameItems.Items.Count; i++)
        {
            AddItem(_allGameItems.Items[i]);
        }
        for (int i = 0; i < _playerItems.Count; i++)
        {
            AddItem(_playerItems[i]);
        }
    }
    public bool AddItem(ItemScriptableObject item)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].HaveItem() == false)
            {
                _cells[i].SetItem(item, Item.InventoryType.Inventory);
                return true;
            }
        }
        return false;
    }
}
