using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScriptableObject : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _skillName;
    [SerializeField] private string _skillDescription;
    [SerializeField] private float _skillCooldown;
    [SerializeField] private Sprite _skillImg;

    public string SkillName { get => _skillName; set => _skillName = value; }
    public string SkillDescription { get => _skillDescription; set => _skillDescription = value; }
    public float SkillCooldown { get => _skillCooldown; set => _skillCooldown = value; }
    public Sprite SkillImg { get => _skillImg;  }
}
