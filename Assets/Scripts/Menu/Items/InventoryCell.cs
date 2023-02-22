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
    public void SetItem(ItemScriptableObject item, Item.InventoryType inventoryType, int count)
    {
        if(_transform== null)
        {
            _transform = transform;
        }
        _item = Instantiate(_itemPrefub, _transform).GetComponent<Item>();
        _item.Set(item, inventoryType, count);
    }
    public void DeleteItem()
    {
        if(_item != null)
        {
            Destroy(_item.gameObject);
        }
    }

}
