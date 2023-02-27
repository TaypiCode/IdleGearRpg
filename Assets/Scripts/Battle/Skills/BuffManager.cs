using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private BattleCharacter _owner;
    private List<BuffSkillScriptable> _buffs = new List<BuffSkillScriptable>();
    private List<Timer> _timers = new List<Timer>();
    private void FixedUpdate()
    {
        BuffWork();
    }
    private void BuffWork()
    {
        float addDamage = 0;
        float addDeffence = 0;
        float addAttackSpeed = 0;
        float addDamagePercent = 0;
        float addDeffencePercent = 0;
        float addAttackSpeedPercent = 0;
        List<BuffSkillScriptable> buffsForRemove = new List<BuffSkillScriptable>();
        for (int i = 0; i < _buffs.Count; i++)
        {
            BuffSkillScriptable buff = _buffs[i];
            if (_timers[i].IsWorking() == false)
            {
                buffsForRemove.Add(_buffs[i]);
                continue;
            }
            switch (buff.GetEffect)
            {
                case BuffSkillScriptable.Effect.ChangeDamage:
                    addDamage += buff.EffectValue;
                    break;
                case BuffSkillScriptable.Effect.ChangeDeffence:
                    addDeffence += buff.EffectValue;
                    break;
                case BuffSkillScriptable.Effect.ChangeAttackSpeed:
                    addAttackSpeed += buff.EffectValue;
                    break;
                case BuffSkillScriptable.Effect.ChangeDamagePercent:
                    addDamagePercent += buff.EffectValue;
                    break;
                case BuffSkillScriptable.Effect.ChangeDeffencePercent:
                    addDeffencePercent += buff.EffectValue;
                    break;
                case BuffSkillScriptable.Effect.ChangeAttackSpeedPercent:
                    addAttackSpeedPercent += buff.EffectValue;
                    break;
                default: continue;
            }
        }
        for (int i = 0; i < buffsForRemove.Count; i++)
        {
            _buffs.Remove(buffsForRemove[i]);
            Destroy(_timers[i]);
            _timers.Remove(_timers[i]);
        }

        addDamage += _owner.Damage / 100 * addDamagePercent;
        _owner.SetUsedDamage(_owner.Damage + addDamage);
        addDeffence += _owner.Deffence / 100 * addDeffencePercent;
        _owner.SetUsedDeffence(_owner.Deffence + addDeffence);
        addAttackSpeed += _owner.AttackSpeed / 100 * addAttackSpeedPercent;
        _owner.SetUsedAttackSpeed(_owner.AttackSpeed + addAttackSpeed);
    }
    public void AddBuff(BuffSkillScriptable buff)
    {
        bool needAdd = true;
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].itemId == buff.itemId)
            {
                _timers[i].SetTimer(_buffs[i].Duration);
                needAdd = false;
            }
        }
        if (needAdd)
        {
            _buffs.Add(buff);
            Timer timer = gameObject.AddComponent<Timer>();
            _timers.Add(timer);
            timer.SetTimer(buff.Duration);
        }
    }

    public void RemoveAllBuffs()
    {
        _buffs.Clear();
        for (int i = 0; i < _buffs.Count; i++)
        {
            Destroy(_timers[i]);
        }
        _timers.Clear();
    }
}
