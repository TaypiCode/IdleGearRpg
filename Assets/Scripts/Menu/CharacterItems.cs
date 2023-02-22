﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
public class CharacterItems : MonoBehaviour
{
    [SerializeField] private float _startHP;
    [SerializeField] private float _startDamage;
    [SerializeField] private float _startDeffence;
    [SerializeField] private float _startAttackSpeed;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _deffenceText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private InventoryCell _headItemCell;
    [SerializeField] private InventoryCell _bodyItemCell;
    [SerializeField] private InventoryCell _handItemCell;
    [SerializeField] private InventoryCell _legItemCell;
    [SerializeField] private InventoryCell _bootsItemCell;
    [SerializeField] private InventoryCell _cloakItemCell;
    [SerializeField] private InventoryCell _neckItemCell;
    [SerializeField] private InventoryCell _earringItemCell;
    [SerializeField] private InventoryCell _ringItemCell;
    [SerializeField] private InventoryCell _mainHandItemCell;
    [SerializeField] private InventoryCell _secondHandItemCell;
    [SerializeField] private InventoryCell _musicianItemCell;
    private void Start()
    {
        CalculateStats();
    }
    public void TrySetItem(ItemScriptableObject item, Item sender)
    {
        if (item is CharacterItemScriptable)
        {
            CharacterItemScriptable scriptable = item as CharacterItemScriptable;
            switch (scriptable.GetPosition)
            {
                case CharacterItemScriptable.Position.Head:
                    SetOrDestroySender(_headItemCell, sender);
                    _headItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Body:
                    SetOrDestroySender(_bodyItemCell, sender);
                    _bodyItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Hand:
                    SetOrDestroySender(_handItemCell, sender);
                    _handItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Leg:
                    SetOrDestroySender(_legItemCell, sender);
                    _legItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Boots:
                    SetOrDestroySender(_bootsItemCell, sender);
                    _bootsItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Cloak:
                    SetOrDestroySender(_cloakItemCell, sender);
                    _cloakItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Neck:
                    SetOrDestroySender(_neckItemCell, sender);
                    _neckItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Earring:
                    SetOrDestroySender(_earringItemCell, sender);
                    _earringItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.Ring:
                    SetOrDestroySender(_ringItemCell, sender);
                    _ringItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.MainHand:
                    SetOrDestroySender(_mainHandItemCell, sender);
                    _mainHandItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.SecondHand:
                    SetOrDestroySender(_secondHandItemCell, sender);
                    _secondHandItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                case CharacterItemScriptable.Position.MusicianInstrument:
                    SetOrDestroySender(_musicianItemCell, sender);
                    _musicianItemCell.SetItem(scriptable, Item.InventoryType.Character, 1);
                    break;
                default: return;
            }

            CalculateStats();
        }
    }
    private void SetOrDestroySender(InventoryCell itemCell, Item sender)
    {
        if (itemCell.GetItem != null)
        {
            if (itemCell.GetItem.ItemScriptable != null)
            {
                sender.Set(itemCell.GetItem.ItemScriptable, Item.InventoryType.Inventory);
                return;
            }
        }
        sender.Remove();
    }
    public void CalculateStats()
    {
        PlayerData.playerDeffence = _startDeffence;
        PlayerData.playerDamage = _startDamage;
        PlayerData.playerHP = _startHP;
        PlayerData.playerAttackSpeed = _startAttackSpeed;
        Item[] itemList = GetItems();
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                CharacterItemScriptable data = itemList[i].ItemScriptable as CharacterItemScriptable;
                PlayerData.playerHP += data.Hp;
                PlayerData.playerDamage += data.Damage;
                PlayerData.playerDeffence += data.Deffence;
                PlayerData.playerAttackSpeed += data.AttackSpeed;
            }
        }
        _hpText.text = "Здоровье: " + Mathf.RoundToInt(PlayerData.playerHP);
        _damageText.text = "Урон: " + StringConverter.ConvertToFormat(PlayerData.playerDamage);
        _deffenceText.text = "Защита: " + StringConverter.ConvertToFormat(PlayerData.playerDeffence) + "%";
        _attackSpeedText.text = "Скорость атаки: " + StringConverter.ConvertToFormat(PlayerData.playerAttackSpeed);
    }
    private Item[] GetItems()
    {
        Item[] itemList = new Item[12]
        {
            _headItemCell.GetItem,
            _bodyItemCell.GetItem,
            _handItemCell.GetItem,
            _legItemCell.GetItem,
            _bootsItemCell.GetItem,
            _cloakItemCell.GetItem,
            _neckItemCell.GetItem,
            _earringItemCell.GetItem,
            _ringItemCell.GetItem,
            _mainHandItemCell.GetItem,
            _secondHandItemCell.GetItem,
            _musicianItemCell.GetItem,
        };
        return itemList;
    }
}
