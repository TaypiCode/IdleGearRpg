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

    public SkillScriptableObject SkillData { get => _skill;  }

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
                    _enemy.GetDamage(skill.Damage);
                    StartCooldown();
                }
                if (_skill is HealthSkillScriptable)
                {
                    HealthSkillScriptable skill = _skill as HealthSkillScriptable;
                    _owner.Health(skill.Health);
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
}
