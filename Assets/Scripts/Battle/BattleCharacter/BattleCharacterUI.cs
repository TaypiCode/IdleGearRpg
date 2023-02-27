using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCharacterUI : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Gradient _hpGradient;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _deffenceText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [Header("Hit anim settings")]
    [SerializeField] private Transform _characterImgCanvas;
    [SerializeField] private float _characterImgHitScale;
    [SerializeField] private float _characterImgHitScaleAnimSpeed;
    [SerializeField] private float _characterImgScaleDelay;
    [SerializeField] private float _hpAnimSpeed;
    [SerializeField] private GameObject _damageTextPrefub;
    [SerializeField] private Transform _damageTextSpawnPos;
    [SerializeField] private Transform _damageTextSpawnPool;

    private float _maxHP;
    private float _endHPAnim;
    private float _currentHPAnim;
    private bool _needAnimHP = false;
    private bool _needAnimHit = false;
    private float _currentCharacterImgScale;
    private Vector3 _characterImgHitStartScale;
    private Timer _characterImgScaleTimer;

    private List<PopUpTextObject> _damageTexts = new List<PopUpTextObject>();
    private void Awake()
    {
        _characterImgScaleTimer = this.gameObject.AddComponent<Timer>();
    }
    private void Start()
    {
        _characterImgHitStartScale = new Vector3(1 + _characterImgHitScale, 1 + _characterImgHitScale, 1);
    }
    private void Update()
    {
        if (_needAnimHP)
        {
            _currentHPAnim = Mathf.Lerp(_currentHPAnim, _endHPAnim, _hpAnimSpeed * Time.deltaTime);
            _hpSlider.value = _currentHPAnim;
            _hpImage.color = _hpGradient.Evaluate(Mathf.Clamp(_currentHPAnim / _maxHP, 0, 1));
            if (_currentHPAnim < 0 || _currentHPAnim > _maxHP)
            {
                _needAnimHP = false;
            }
        }
        if (_needAnimHit && _characterImgScaleTimer.IsWorking() == false)
        {
            _currentCharacterImgScale = Mathf.Lerp(_currentCharacterImgScale, 1, _characterImgHitScaleAnimSpeed * Time.deltaTime);
            _characterImgCanvas.localScale = new Vector3(_currentCharacterImgScale, _currentCharacterImgScale, 1);
            if (_characterImgCanvas.localScale == Vector3.one)
            {
                _needAnimHit = false;
            }
        }
    }
    public void SetStartValues(float hp, float maxHP, float damage, float deffence, float attackSpeed)
    {
        _needAnimHP = false;
        _maxHP = maxHP;
        _hpSlider.maxValue = maxHP;
        SetHP(hp, false);
        SetStatDamageText(damage, false);
        SetStatDeffenceText(deffence, false);
        SetStatAttackSpeedText(attackSpeed, false);
    }
    public void SetHP(float val, bool animUsed)
    {
        if (animUsed == false)
        {
            _hpSlider.value = val;
            _currentHPAnim = val;
        }
        _hpImage.color = _hpGradient.Evaluate(Mathf.Clamp(val / _maxHP, 0, 1));
        _hpText.text = Mathf.RoundToInt(val) + "/" + Mathf.RoundToInt(_maxHP);

    }
    public void SetStatDamageText(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _damageText.text = StringConverter.ConvertToFormat(val);
        }
    }
    public void SetStatDeffenceText(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _deffenceText.text = StringConverter.ConvertToFormat(val) + "%";
        }
    }
    public void SetStatAttackSpeedText(float val, bool showEffect)
    {
        if (showEffect == false)
        {
            _attackSpeedText.text = StringConverter.ConvertToFormat(val);
        }
    }
    public void ShowDamageEffect(float fromHP, float damage)
    {
        _endHPAnim = fromHP - damage;
        _needAnimHP = true;
        ShowDamageText(damage.ToString());
        ShowHitAnim();
    }
    private void ShowHitAnim()
    {
        _currentCharacterImgScale = 1 + _characterImgHitScale;
        _characterImgCanvas.localScale = _characterImgHitStartScale;
        _characterImgScaleTimer.SetTimer(_characterImgScaleDelay);
        _needAnimHit = true;
    }
    private void ShowDamageText(string val)
    {
        PopUpTextObject damageText = null;
        for (int i = 0; i < _damageTexts.Count; i++)
        {
            if (_damageTexts[i].IsShowing() == false)
            {
                damageText = _damageTexts[i];
                break;
            }
        }
        if (damageText == null)
        {
            damageText = Instantiate(_damageTextPrefub, _damageTextSpawnPool).GetComponent<PopUpTextObject>();
            _damageTexts.Add(damageText);
        }
        damageText.ShowText(val, _damageTextSpawnPos.position);
    }
}
