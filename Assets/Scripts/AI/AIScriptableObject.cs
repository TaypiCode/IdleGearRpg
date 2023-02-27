using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AIScriptable", menuName = "ScriptableObjects/AI", order = 1)]
public class AIScriptableObject : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _characterImg;
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _deffence;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private SkillScriptableObject[] _skills;

    public string CharacterName { get => _characterName;  }
    public Sprite CharacterImg { get => _characterImg;  }
    public float Hp { get => _hp; }
    public float Damage { get => _damage;  }
    public float Deffence { get => _deffence; }
    public float AttackSpeed { get => _attackSpeed;  }
    public SkillScriptableObject[] Skills { get => _skills;  }
}
