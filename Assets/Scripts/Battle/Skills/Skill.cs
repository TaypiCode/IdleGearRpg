using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Skill : MonoBehaviour
{
    [SerializeField] private SkillCooldownUI _skillCooldownUI;
    private SkillScriptableObject _skill;
    private BattleCharacter _owner;
    private BattleCharacter _enemy;
    private Timer _cooldownTimer;
    private SkillOverview _skillOverview;
    public SkillScriptableObject SkillData { get => _skill;  }
    private void Start()
    {
        _skillOverview = FindObjectOfType<SkillOverview>();
    }
    private void FixedUpdate()
    {
        UpdateCooldownUI();
    }
    public void Set(SkillScriptableObject skill, BattleCharacter owner, BattleCharacter enemy)
    {
        if (_cooldownTimer != null)
        {
            Destroy(_cooldownTimer);
        }
        _cooldownTimer = gameObject.AddComponent<Timer>();
        _skill = skill;
        _owner = owner;
        _enemy = enemy;
    }
    public void Use()
    {
        if(_skill != null) {
            if (_cooldownTimer.IsWorking() == false)
            {
                if (_skill is AttackSkillScriptable)
                {
                    AttackSkillScriptable skill = _skill as AttackSkillScriptable;
                    switch (skill.GetVariant)
                    {
                        case AttackSkillScriptable.Variant.Simple:
                            _enemy.GetDamage(skill.Damage);
                            break;
                        case AttackSkillScriptable.Variant.Percent:
                            float dmg = (_owner.UsedDamage / 100) * skill.Damage;
                            _enemy.GetDamage(dmg);
                            break;
                    }
                    StartCooldown();
                }
                if (_skill is HealthSkillScriptable)
                {
                    HealthSkillScriptable skill = _skill as HealthSkillScriptable;
                    switch (skill.GetVariant)
                    {
                        case HealthSkillScriptable.Variant.Simple:
                            _owner.Health(skill.Health);
                            break;
                        case HealthSkillScriptable.Variant.Percent:
                            float hp = (_owner.MaxHP / 100) * skill.Health;
                            _owner.Health(hp);
                            break;
                    }
                    StartCooldown();
                }
                if (_skill is BuffSkillScriptable)
                {
                    BuffSkillScriptable skill = _skill as BuffSkillScriptable;
                    switch (skill.GetVariant)
                    {
                        case BuffSkillScriptable.Variant.Buff:
                            _owner.BuffManager.AddBuff(skill);
                            break;
                        case BuffSkillScriptable.Variant.Debuff:
                            _enemy.BuffManager.AddBuff(skill);
                            break;
                    }
                    StartCooldown();
                }
            }
        }
    }
    private void StartCooldown()
    {
        _cooldownTimer.SetTimer(_skill.SkillCooldown);
        StartCooldownUI();
    }
    private void StartCooldownUI()
    {
        if (_skillCooldownUI != null)
        {
            _skillCooldownUI.StartCooldown(_skill.SkillCooldown);
            UpdateCooldownUI();
        }
    }
    private void UpdateCooldownUI()
    {
        if(_skillCooldownUI != null)
        {
            _skillCooldownUI.SetCooldownTime(_cooldownTimer.GetTime());
        }
    }
    public void Remove()
    {
        Destroy(_cooldownTimer);
        Destroy(this);
    }
    public bool CanUse()
    {
        return !_cooldownTimer.IsWorking();
    }
    public void ShowOverview()
    {
        _skillOverview.ShowOverview(_skill);
    }
    public void HideOverview()
    {
        _skillOverview.HideOverview();
    }
}
