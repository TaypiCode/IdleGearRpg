using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAutoUse : MonoBehaviour
{
    [SerializeField] private BattleCharacter _character;
    [SerializeField] private BattleCharacter _enemy;
    [SerializeField] private bool _needAutoUse;
    private Skill[] _skills;
    private void Update()
    {
        if (_needAutoUse)
        {
            for (int i = 0; i < _skills.Length; i++)
            {
                if (_skills[i].SkillData is AttackSkillScriptable)
                {
                    if (_skills[i].CanUse())
                    {
                        _skills[i].Use();
                    }
                }
                if (_skills[i].SkillData is HealthSkillScriptable)
                {
                    if(_character.Hp < _character.MaxHP / 2)
                    {
                        _skills[i].Use();
                    }
                }
                if (_skills[i].SkillData is BuffSkillScriptable)
                {
                    _skills[i].Use();
                }
            }
        }
    }
    public void SetSkills(Skill[] skills)
    {
        _skills = skills;
    }
}
