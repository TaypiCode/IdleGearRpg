using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCharacterStats : MonoBehaviour
{

    [SerializeField] private float _startHP;
    [SerializeField] private float _startDamage;
    [SerializeField] private float _startDeffence;
    [SerializeField] private float _startAttackSpeed;

    public float StartHP { get => _startHP;  }
    public float StartDamage { get => _startDamage;  }
    public float StartDeffence { get => _startDeffence; }
    public float StartAttackSpeed { get => _startAttackSpeed;  }
}
