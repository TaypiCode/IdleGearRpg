using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuffSkillScriptable", menuName = "ScriptableObjects/Skills/BuffSkill", order = 1)]
public class BuffSkillScriptable : SkillScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private Effect _effect;
    [SerializeField] private float _effectValue;

    public float Duration { get => _duration; }
    public float EffectValue { get => _effectValue;  }
    public Effect GetEffect { get => _effect; }

    public enum Effect
    {
        ChangeDamage,
        ChangeDeffence,
        ChangeAttackSpeed,
        ChangeDamagePercent,
        ChangeDeffencePercent,
        ChangeAttackSpeedPercent
    }
}
