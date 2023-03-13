using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonLootItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _lootInfoText;
    public void Set(ItemScriptableObject itemData, int count, float dropChance)
    {
        _item.Set(itemData, Item.InventoryType.OnlyShow, itemData.StartGrade, 0, count, false);
        _lootInfoText.text = "Количество: " + count + "\n" + "Шанс: " + StringConverter.ConvertToFormat(dropChance) + "%";
    }
}
