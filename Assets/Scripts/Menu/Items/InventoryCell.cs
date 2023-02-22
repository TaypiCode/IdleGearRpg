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
    public void SetItem(ItemScriptableObject item)
    {
        _item = Instantiate(_itemPrefub, _transform).GetComponent<Item>();
        if (item is ArmorScriptable)
        {
            _item.gameObject.AddComponent<ArmorItem>().Set(item,_item);
            Debug.Log(_item);
        }
    }
    public void DeleteItem()
    {
        if(_item != null)
        {
            Destroy(_item.gameObject);
        }
    }
    public bool HaveItem()
    {
        return _item != null;
    }
}
