using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleCharacterUI : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _deffenceText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private float _hpAnimSpeed;
    private float _maxHP;
    private float _endHPAnim;
    private float _currentHPAnim;
    private bool _needAnimHP = false;
    private void Update()
    {
        if (_needAnimHP)
        {
            _currentHPAnim = Mathf.Lerp(_currentHPAnim, _endHPAnim, _hpAnimSpeed * Time.deltaTime);
            _hpSlider.value = _currentHPAnim;
            if(_currentHPAnim < 0 || _currentHPAnim > _maxHP)
            {
                _needAnimHP = false;
            }
        }
    }
    public void SetStartValues(float hp, float maxHP, float damage, float deffence, float attackSpeed)
    {
        _maxHP = maxHP;
        _hpSlider.maxValue = maxHP;
        SetHP(hp, false);
        SetDamage(damage, false);
        SetDeffence(deffence, false);
        SetAttackSpeed(attackSpeed, false);
    }
    public void SetHP(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _currentHPAnim = val;
            _endHPAnim = val;
            _hpSlider.value = val;
        }
        else
        {
            _endHPAnim = val;
        }
        _hpText.text = Mathf.RoundToInt(val) + "/" + Mathf.RoundToInt(_maxHP);
        _needAnimHP = showEffect;
    }
    public void SetDamage(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _damageText.text = ConvertToFormat(val);
        }
    }
    public void SetDeffence(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _deffenceText.text = ConvertToFormat(val);
        }
    }
    public void SetAttackSpeed(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _attackSpeedText.text = ConvertToFormat(val);
        }
    }
    private string ConvertToFormat(float val)
    {
        return System.String.Format("{0:0.00}", System.Math.Round(val, 2));
    }
}
