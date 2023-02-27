using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
public class CharacterItems : MonoBehaviour
{
    [SerializeField] private AllGameItems _allGameItems;
    [SerializeField] private StartCharacterStats _startCharacterStats;
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
    public void SetItemsFromSave(string[] ids, int[] itemGrade)
    {
        ItemScriptableObject[] allItems = _allGameItems.Items.ToArray();
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] != null)
            {
                for(int j = 0; j < allItems.Length; j++)
                {
                    if (ids[i] == allItems[j].itemId)
                    {
                        TrySetItem(allItems[j], null, itemGrade[i]);
                        break;
                    }
                }
            }
        }
    }
    public void TrySetItem(ItemScriptableObject item, Item sender, int grade)
    {
        if (item is CharacterItemScriptable)
        {
            CharacterItemScriptable scriptable = item as CharacterItemScriptable;
            switch (scriptable.GetPosition)
            {
                case CharacterItemScriptable.Position.Head:
                    SetOrDestroySender(_headItemCell, sender);
                    _headItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Body:
                    SetOrDestroySender(_bodyItemCell, sender);
                    _bodyItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Hand:
                    SetOrDestroySender(_handItemCell, sender);
                    _handItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Leg:
                    SetOrDestroySender(_legItemCell, sender);
                    _legItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Boots:
                    SetOrDestroySender(_bootsItemCell, sender);
                    _bootsItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Cloak:
                    SetOrDestroySender(_cloakItemCell, sender);
                    _cloakItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Neck:
                    SetOrDestroySender(_neckItemCell, sender);
                    _neckItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Earring:
                    SetOrDestroySender(_earringItemCell, sender);
                    _earringItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.Ring:
                    SetOrDestroySender(_ringItemCell, sender);
                    _ringItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.MainHand:
                    SetOrDestroySender(_mainHandItemCell, sender);
                    _mainHandItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.SecondHand:
                    SetOrDestroySender(_secondHandItemCell, sender);
                    _secondHandItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                case CharacterItemScriptable.Position.MusicianInstrument:
                    SetOrDestroySender(_musicianItemCell, sender);
                    _musicianItemCell.SetItem(scriptable, Item.InventoryType.Character, grade);
                    break;
                default: return;
            }

            CalculateStats();
        }
    }
    private void SetOrDestroySender(InventoryCell itemCell, Item sender)
    {
        if (sender != null)
        {
            sender.Remove();
            if (itemCell.GetItem != null)
            {
                if (itemCell.GetItem.ItemScriptable != null)
                {
                    FindObjectOfType<Inventory>().CreateItem(itemCell.GetItem.ItemScriptable, itemCell.GetItem.ItemGrade, itemCell.GetItem.ItemsCount);
                }

            }
        }
    }
    public void CalculateStats()
    {
        PlayerData.playerDeffence = _startCharacterStats.StartDeffence;
        PlayerData.playerDamage = _startCharacterStats.StartDamage;
        PlayerData.playerHP = _startCharacterStats.StartHP;
        PlayerData.playerAttackSpeed = _startCharacterStats.StartAttackSpeed;
        Item[] itemList = GetItems();
        List<SkillScriptableObject> skillList = new List<SkillScriptableObject>();
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                CharacterItemScriptable data = itemList[i].ItemScriptable as CharacterItemScriptable;
                PlayerData.playerHP += data.Hp;
                PlayerData.playerDamage += data.Damage;
                PlayerData.playerDeffence += data.Deffence;
                PlayerData.playerAttackSpeed += data.AttackSpeed;
                if(data.SkillScriptable != null)
                {
                    skillList.Add(data.SkillScriptable);
                }
            }
        }
        PlayerData.playerSkills = skillList.ToArray();
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
    public string[] GetItemsId()
    {
        Item[] items = GetItems();
        string[] ids = new string[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                ids[i] = items[i].ItemScriptable.itemId;
            }
            else
            {
                ids[i] = null;
            }
        }
        return ids;
    }
    public int[] GetItemsGrade()
    {
        Item[] items = GetItems();
        int[] grades = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                grades[i] = items[i].ItemGrade;
            }
            else
            {
                grades[i] = -1;
            }
        }
        return grades;
    }
}
