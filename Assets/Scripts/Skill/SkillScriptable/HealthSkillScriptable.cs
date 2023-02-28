using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthSkillScriptable", menuName = "ScriptableObjects/Skills/HealthSkill", order = 1)]
public class HealthSkillScriptable : SkillScriptableObject
{
    [SerializeField] private float _health;

    [SerializeField] private Variant _variant;
    public enum Variant
    {
        Simple,
        Percent
    }

    public float Health { get => _health; }
    public Variant GetVariant { get => _variant; }
}
