using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemOverview : MonoBehaviour
{
    [Header("Overview Item")]
    [SerializeField] private GameObject _itemOverviewCanvas;
    [SerializeField] private Image _itemOverviewImage;
    [SerializeField] private Image _itemOverviewGradeImage;
    [SerializeField] private GameObject _itemOverviewUpgradeProgressCanvas;
    [SerializeField] private Slider _itemOverviewUpgradeProgressSlider;
    [SerializeField] private Image _itemOverviewUpgradeProgressSliderFillImg;
    [SerializeField] private TextMeshProUGUI _itemOverviewUpgradeProgressText;
    [SerializeField] private TextMeshProUGUI _itemOverviewText;
    public void ShowItemOverview(Item item)
    {
        string newString = "\n";
        _itemOverviewText.text = item.ItemScriptable.ItemName + newString;
        _itemOverviewImage.sprite = item.ItemScriptable.Sprite;
        _itemOverviewGradeImage.color = ItemGradeColor.GetGradeColor(item.ItemGrade);
        if (item.ItemScriptable is UpgradeItemScriptable)
        {
            UpgradeItemScriptable upgradeData = item.ItemScriptable as UpgradeItemScriptable;
            if (item.ItemGrade == item.ItemScriptable.MaxGrade)
            {
                _itemOverviewUpgradeProgressSlider.value = _itemOverviewUpgradeProgressSlider.maxValue;
                _itemOverviewUpgradeProgressText.text = "Макс";
            }
            else
            {
                _itemOverviewUpgradeProgressSlider.maxValue = upgradeData.NeedExpForUpgradeByGrade[item.ItemGrade - 1];
                _itemOverviewUpgradeProgressSlider.value = item.UpgradeXP;
                _itemOverviewUpgradeProgressText.text = _itemOverviewUpgradeProgressSlider.value + "/" + _itemOverviewUpgradeProgressSlider.maxValue;
            }
            _itemOverviewUpgradeProgressSliderFillImg.color = ItemGradeColor.GetGradeColor(item.ItemGrade);
            _itemOverviewUpgradeProgressCanvas.SetActive(true);

        }
        else
        {
            _itemOverviewUpgradeProgressCanvas.SetActive(false);
        }
        if (item.ItemScriptable is CharacterItemScriptable)
        {
            CharacterItemScriptable data = item.ItemScriptable as CharacterItemScriptable;
            int grade = item.ItemGrade;
            if (data.Hp != 0)
            {
                _itemOverviewText.text += "Здоровье: " + Mathf.RoundToInt(CharacterItems.CalculateStatByGrade(data.Hp, grade)) + newString;
            }
            if (data.Damage != 0)
            {
                _itemOverviewText.text += "Урон: " + StringConverter.ConvertToFormat((CharacterItems.CalculateStatByGrade(data.Damage, grade))) + newString;
            }
            if (data.Deffence != 0)
            {
                _itemOverviewText.text += "Защита: " + StringConverter.ConvertToFormat((CharacterItems.CalculateStatByGrade(data.Deffence, grade))) + "%" + newString;
            }
            if (data.AttackSpeed != 0)
            {
                _itemOverviewText.text += "Скорость атаки: " + StringConverter.ConvertToFormat((CharacterItems.CalculateStatByGrade(data.AttackSpeed, grade))) + newString;
            }

        }
        else if (item.ItemScriptable is MiscItemScriptable)
        {
            MiscItemScriptable data = item.ItemScriptable as MiscItemScriptable;
            _itemOverviewText.text += data.Info + newString;
        }
        _itemOverviewText.text += "Количество: " + item.ItemsCount;
        _itemOverviewCanvas.SetActive(true);
    }
    public void HideItemOverview()
    {
        _itemOverviewCanvas.SetActive(false);
    }
}
