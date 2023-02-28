using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private BattleCharacter _owner;
    [SerializeField] private GameObject _buffPrefub;
    [SerializeField] private Transform _buffSpawn;
    private List<Buff> _buffs = new List<Buff>();
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
        List<Buff> buffsForRemove = new List<Buff>();
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i] == null)
            {
                buffsForRemove.Add(_buffs[i]);
                continue;
            }
            BuffSkillScriptable buff = _buffs[i].BuffScriptable;
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
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i] != null)
            {
                if (_buffs[i].BuffScriptable.itemId == buff.itemId)
                {
                    _buffs[i].Remove();
                    break;
                }
            }
        }
        Buff buffObj = Instantiate(_buffPrefub, _buffSpawn).GetComponent<Buff>();
        _buffs.Add(buffObj);
        buffObj.Set(buff);
    }

    public void RemoveAllBuffs()
    {
        for (int i = 0; i < _buffs.Count; i++)
        {
            _buffs[i].Remove();
        }
    }

}
