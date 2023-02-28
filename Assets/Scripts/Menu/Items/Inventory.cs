using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AllGameItems _allGameItems;
    [SerializeField] private ItemOverview _itemOverview;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();

    private void Start()
    {
        for (int i = 0; i < _allGameItems.Items.Count; i++) //for test
        {
            //CreateItem(_allGameItems.Items[i], 1, 1);
        }
        AddItemsFromDungeon();

    }
    public void CreateFromSave(string[] playerItemsId, int[] playerItemsItemGrade,int[] xp, int[] playerItemsCount)
    {
        List<ItemScriptableObject> allItems = _allGameItems.Items;

        for (int i = 0; i < playerItemsId.Length; i++)
        {
            for (int j = 0; j < allItems.Count; j++)
            {
                if (playerItemsId[i] == allItems[j].itemId)
                {
                    CreateItem(allItems[j], playerItemsItemGrade[i], xp[i], playerItemsCount[i]);
                    break;
                }
            }
        }
    }
    public bool CreateItem(ItemScriptableObject item, int grade, int xp, int count)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (count > 0)
            {
                if (_cells[i].GetItem != null)
                {
                    if (_cells[i].GetItem.ItemScriptable.itemId == item.itemId)
                    {
                        if (_cells[i].GetItem.ItemsCount != item.CountInStock)
                        {
                            if (_cells[i].GetItem.ItemsCount + count <= item.CountInStock)
                            {

                                _cells[i].SetItem(item, Item.InventoryType.Inventory, grade, xp, _cells[i].GetItem.ItemsCount + count);
                                return true;
                            }
                            else
                            {
                                int added = item.CountInStock - _cells[i].GetItem.ItemsCount;
                                count -= added;
                                _cells[i].SetItem(item, Item.InventoryType.Inventory, grade, xp, item.CountInStock);
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
                    if (count <= item.CountInStock)
                    {
                        _cells[i].SetItem(item, Item.InventoryType.Inventory, grade, xp, count);
                        return true;
                    }
                    else
                    {
                        _cells[i].SetItem(item, Item.InventoryType.Inventory, grade, xp, item.CountInStock);
                        count = count - item.CountInStock;
                    }
                }
            }
        }
        return false;
    }
    private void AddItemsFromDungeon()
    {
        ItemScriptableObject[] items = PlayerData.lootRewardFromDungeon;
        int[] count = PlayerData.lootRewardCountFromDungeon;
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                CreateItem(items[i], items[i].StartGrade, 0, count[i]);
            }
        }
        PlayerData.lootRewardFromDungeon = null;
        PlayerData.lootRewardCountFromDungeon = null;
    }
    
    
    private Item[] GetItems()
    {
        List<Item> items = new List<Item>();
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].GetItem != null)
            {
                items.Add(_cells[i].GetItem);
            }
        }
        return items.ToArray();
    }
    public int[] GetPlayerItemsItemCount()
    {
        List<int> items = new List<int>();
        Item[] inventoryItems = GetItems();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            items.Add(inventoryItems[i].ItemsCount);
        }
        return items.ToArray();
    }
    public string[] GetPlayerItemsItemId()
    {
        List<string> items = new List<string>();
        Item[] inventoryItems = GetItems();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            items.Add(inventoryItems[i].ItemScriptable.itemId);
        }
        return items.ToArray();
    }
    public int[] GetPlayerItemsGrade()
    {
        List<int> grade = new List<int>();
        Item[] inventoryItems = GetItems();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            grade.Add(inventoryItems[i].ItemGrade);
        }
        return grade.ToArray();
    }
    public int[] GetPlayerItemsXP()
    {
        List<int> xp = new List<int>();
        Item[] inventoryItems = GetItems();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            xp.Add(inventoryItems[i].UpgradeXP);
        }
        return xp.ToArray();
    }
}
