using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefub;
    private Item _item = null;
    private Transform _transform;

    public Item GetItem { get => _item;  }

    private void Start()
    {
        _transform = transform;
    }
    public Item SetItem(ItemScriptableObject item, Item.InventoryType inventoryType, int itemGrade = 1, int xp = 0, int count = 1)
    {
        if(_transform== null)
        {
            _transform = transform;
        }
        _item = Instantiate(_itemPrefub, _transform).GetComponent<Item>();
        _item.Set(item, inventoryType, itemGrade, xp, count);
        return _item;
    }
    public void DeleteItem(bool needRecalcPlayerStat = false)
    {
        if(_item != null)
        {
            Destroy(_item.gameObject);
            _item = null;
            if (needRecalcPlayerStat)
            {
                FindObjectOfType<CharacterItems>().CalculateStats();
            }
        }
    }

}
