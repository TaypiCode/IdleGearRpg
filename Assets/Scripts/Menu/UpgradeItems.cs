using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItems : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private InventoryCell _mainItemCell;
    [SerializeField] private InventoryCell _secondItemCell;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private Image _xpSliderFillImg;
    [SerializeField] private TextMeshProUGUI _xpSliderText;
    [SerializeField] private GameObject _upgradeBtn;
    public bool TrySetItem(Item item)
    {
        if(_mainItemCell.GetItem == null)
        {
            if (SetMainItem(item))
            {
                return true;
            }
        }
        else if(_secondItemCell.GetItem == null)
        {
            if (_mainItemCell.GetItem.ItemGrade != _mainItemCell.GetItem.ItemScriptable.MaxGrade)
            {
                if (_mainItemCell.GetItem.ItemGrade <= item.ItemGrade)
                {
                    if (SetSecondItem(item))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool SetMainItem(Item item)
    {
        if (item.ItemScriptable is UpgradeItemScriptable)
        {
            if (item.ItemGrade != item.ItemScriptable.MaxGrade)
            {
                _mainItemCell.SetItem(item.ItemScriptable, Item.InventoryType.Upgrade, item.ItemGrade, item.UpgradeXP, 1);
                UpdateInfo();
                return true;
            }
        }
        return false;
    }
    private bool SetSecondItem(Item item)
    {
        if (item.ItemScriptable is UpgradeItemScriptable)
        {
            _secondItemCell.SetItem(item.ItemScriptable, Item.InventoryType.Upgrade, item.ItemGrade, item.UpgradeXP, 1);
            UpdateInfo();
            return true;
        }
        return false;
    }
    public void Upgrade()
    {
        if (_mainItemCell.GetItem != null)
        {
            Item mainItem = _mainItemCell.GetItem;
            UpgradeItemScriptable mainItemUpgradeData = mainItem.ItemScriptable as UpgradeItemScriptable;
            if (_secondItemCell.GetItem != null)
            {
                Item secondItem = _secondItemCell.GetItem;
                UpgradeItemScriptable secondItemUpgradeData = secondItem.ItemScriptable as UpgradeItemScriptable;
                int giveXp = secondItemUpgradeData.GiveExpByGrade[secondItem.ItemGrade - 1];
                int[] newData = GetNewXPAndGrade(mainItem, giveXp);
                int newXp = newData[1];
                int newGrade = newData[0];
                _mainItemCell.GetItem.Set(mainItem.ItemScriptable, Item.InventoryType.Upgrade, newGrade, newXp, 1);
                _secondItemCell.DeleteItem();
                UpdateInfo();
            }
        }
    }
    private void UpdateInfo()
    {
        
        if(_mainItemCell.GetItem != null)
        {
            Item mainItem = _mainItemCell.GetItem;
            UpgradeItemScriptable mainItemUpgradeData = mainItem.ItemScriptable as UpgradeItemScriptable;
            if (_secondItemCell.GetItem != null)
            {
                Item secondItem = _secondItemCell.GetItem;
                UpgradeItemScriptable secondItemUpgradeData = secondItem.ItemScriptable as UpgradeItemScriptable;
                int giveXp = secondItemUpgradeData.GiveExpByGrade[secondItem.ItemGrade - 1];
                _infoText.text = "Перевести " + giveXp + " опыта";
                int[] newData = GetNewXPAndGrade(mainItem, giveXp);
                int newXp = newData[1];
                int newGrade = newData[0];
                
                if(newGrade == mainItemUpgradeData.MaxGrade)
                {
                    _xpSlider.maxValue = 1;
                    _xpSlider.value = 1;
                    _xpSliderText.text = "Макс";
                }
                else
                {
                    _xpSlider.maxValue = mainItemUpgradeData.NeedExpForUpgradeByGrade[newGrade - 1];
                    _xpSlider.value = newXp;
                    _xpSliderText.text = _xpSlider.value + "/" + _xpSlider.maxValue;
                }
                _xpSliderFillImg.color = ItemGradeColor.GetGradeColor(newGrade);
                _xpSlider.gameObject.SetActive(true);
                _upgradeBtn.SetActive(true);
            }
            else
            {
                
                if (mainItem.ItemGrade == mainItemUpgradeData.MaxGrade)
                {
                    _xpSlider.maxValue = 1;
                    _xpSlider.value = 1;
                    _xpSliderText.text = "Макс";
                }
                else
                {
                    _infoText.text = ""; 
                    _xpSlider.maxValue = mainItemUpgradeData.NeedExpForUpgradeByGrade[mainItem.ItemGrade - 1];
                    _xpSlider.value = mainItem.UpgradeXP;
                    _xpSliderText.text = _xpSlider.value + "/" + _xpSlider.maxValue;
                }
                _xpSliderFillImg.color = ItemGradeColor.GetGradeColor(mainItem.ItemGrade);
                _xpSlider.gameObject.SetActive(true);
                _upgradeBtn.SetActive(false);
            }
            
        }
        else
        {
            _infoText.text = "";
            _xpSlider.gameObject.SetActive(false);
            _upgradeBtn.SetActive(false);
        }
    }
    private int[] GetNewXPAndGrade(Item item, int addXP)
    {
        int[] newVal = new int[2];
        int newXP = item.UpgradeXP;
        int newGrade = item.ItemGrade;
        UpgradeItemScriptable upgradeData = item.ItemScriptable as UpgradeItemScriptable;
        bool needCheckForNewGrade = true;
        int needXP = upgradeData.NeedExpForUpgradeByGrade[newGrade - 1];
        while (needCheckForNewGrade)
        {
            needXP = upgradeData.NeedExpForUpgradeByGrade[newGrade - 1];
            
            if(newXP + addXP > needXP)
            {
                int currentXp = newXP;
                newXP += addXP;
                addXP -= needXP - currentXp;
                newXP = 0;
                newGrade++;
            }
            else if(newXP + addXP < needXP)
            {
                newXP += addXP;
                needCheckForNewGrade = false;
            }
            else // == 0
            {
                newXP = 0;
                addXP = 0;
                newGrade++;
                needCheckForNewGrade = false;
            }
            if(newGrade >= upgradeData.MaxGrade)
            {
                newGrade = upgradeData.MaxGrade;
                newXP = 0;
                break;
            }
        }
        newVal[0] = newGrade;
        newVal[1] = newXP;
        return newVal;
    }
    public void UpdateAfterUndress()
    {
        if (_mainItemCell.GetItem == null)
        {
            if (_secondItemCell.GetItem != null)
            {
                _secondItemCell.GetItem.Use();//undress
            }
        }
        UpdateInfo();
    }
    public void UndressItems()
    {
        _secondItemCell.GetItem?.Use();
        _mainItemCell.GetItem?.Use();
    }
    public void Close()
    {
        UndressItems();
        _canvas.SetActive(false);
    }
    public bool IsActive()
    {
        return _canvas.activeSelf;
    }
}
