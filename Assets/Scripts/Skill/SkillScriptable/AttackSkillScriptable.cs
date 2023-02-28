using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackSkillScriptable", menuName = "ScriptableObjects/Skills/AttackSkill", order = 1)]
public class AttackSkillScriptable : SkillScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private Variant _variant;
    public enum Variant
    {
        Simple,
        Percent
    }

    public float Damage { get => _damage; }
    public Variant GetVariant { get => _variant;  }
}
