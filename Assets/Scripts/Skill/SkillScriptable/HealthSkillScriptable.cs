using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthSkillScriptable", menuName = "ScriptableObjects/Skills/HealthSkill", order = 1)]
public class HealthSkillScriptable : SkillScriptableObject
{
    [SerializeField] private float _health;

    public float Health { get => _health; }
}
