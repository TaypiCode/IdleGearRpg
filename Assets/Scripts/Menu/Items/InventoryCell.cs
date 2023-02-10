using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefub;
    private Item _item = null;
    private Transform _transform;
    public void SetItem<T>(System.Type item)
    {
        _item = (Item)Instantiate(_itemPrefub, _transform).AddComponent(item);
    }
    public void DeleteItem()
    {
        if(_item != null)
        {
            Destroy(_item.gameObject);
        }
    }
}
