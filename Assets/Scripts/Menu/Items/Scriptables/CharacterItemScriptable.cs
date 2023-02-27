using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterItemScriptable", menuName = "ScriptableObjects/Items/CharacterItem", order = 1)]
public class CharacterItemScriptable : ItemScriptableObject
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _deffence;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private Position _position;
    [SerializeField] private SkillScriptableObject _skillScriptable;
    public enum Position
    {
        Head,
        Body,
        Hand,
        Leg,
        Boots,
        Cloak,
        Neck,
        Earring,
        Ring,
        MainHand,
        SecondHand,
        MusicianInstrument
    }

    public float Deffence { get => _deffence; }
    public Position GetPosition { get => _position;  }
    public float Hp { get => _hp; }
    public float Damage { get => _damage;  }
    public float AttackSpeed { get => _attackSpeed;  }
    public SkillScriptableObject SkillScriptable { get => _skillScriptable;  }
}
