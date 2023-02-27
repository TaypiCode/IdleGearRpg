using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackSkillScriptable", menuName = "ScriptableObjects/Skills/AttackSkill", order = 1)]
public class AttackSkillScriptable : SkillScriptableObject
{
    [SerializeField] private float _damage;

    public float Damage { get => _damage; }
}
