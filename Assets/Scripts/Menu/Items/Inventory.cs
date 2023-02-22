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
        AddItem(_allGameItems.Items[0]);
    }
    public void AddItem(ItemScriptableObject item)
    {
        InventoryCell cell = null;
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].HaveItem() == false)
            {
                cell = _cells[i];
                break;
            }
        }
        if(cell != null)
        {
            cell.SetItem(item);
        }
    }
}
