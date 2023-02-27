using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] private BattleCharacterUI _battleCharacterUI;
    [SerializeField] private BattleCharacter _enemy;
    [SerializeField] private SkillsCreator _skillsCreator;
    [SerializeField] private SkillAutoUse _skillAutoUse;
    [SerializeField] private BuffManager _buffManager;
    private float _hp, _maxHP;
    private float _damage;
    private float _deffence;
    private float _attackSpeed; //attacksPerMinute
    private float _usedDamage;
    private float _usedDeffence;
    private float _usedAttackSpeed; //attacksPerMinute
    private AliveState _aliveState;
    public UnityEvent deathEvent;
    private enum AliveState
    {
        Alive,
        Dead
    }

    private Timer _attackTimer;

    public float Hp { get => _hp; }
    public float MaxHP { get => _maxHP;  }
    public float Damage { get => _damage; }
    public float Deffence { get => _deffence; }
    public float AttackSpeed { get => _attackSpeed;  }
    public BuffManager BuffManager { get => _buffManager; }

    private void Awake()
    {
        _attackTimer = this.gameObject.AddComponent<Timer>();
    }
    private void Update()
    {
        if (_aliveState == AliveState.Alive)
        {
            if (_enemy.IsAlive())
            {
                if (_attackTimer.IsWorking() == false)
                {
                    SimpleAttack();
                }
            }
        }
    }
    public void SetStartStats(float hp, float damage, float deffence, float attackSpeed, SkillScriptableObject[] skills)
    {
        _maxHP = hp;
        _hp = _maxHP;
        _damage = damage;
        _deffence = deffence;
        _attackSpeed = attackSpeed;
        _usedDamage = damage;
        _usedDeffence = deffence;
        _usedAttackSpeed = attackSpeed;

        UpdateStatsText(false);

        CooldownSimpleAttack();

        _aliveState = AliveState.Alive;
        _buffManager.RemoveAllBuffs();
        _skillAutoUse.SetSkills(_skillsCreator.CreateSkills(skills, this, _enemy));

    }
    private void UpdateStatsText(bool fromBuffs)
    {
        _battleCharacterUI.SetStartValues(_hp, _maxHP, _usedDamage, _usedDeffence, _usedAttackSpeed, fromBuffs);
    }
    private void SimpleAttack()
    {
        if (_aliveState == AliveState.Alive)
        {
            if (_enemy.IsAlive())
            {
                _enemy.GetDamage(_usedDamage);
                CooldownSimpleAttack();
            }
        }
    }
    private void CooldownSimpleAttack()
    {
        _attackTimer.SetTimer(60 / _usedAttackSpeed); //attacks per min
    }
    public void GetDamage(float val)
    {
        if(_usedDeffence > 0)
        {
            if(_usedDeffence >= 100)
            {
                val = val * 0.01f; //1% 
            }
            else
            {
                val = val - val * _usedDeffence / 100;
            }
        }
        _battleCharacterUI.ShowChangeHPEffect(_hp, -val);
        _hp -= val;
        if (_hp <= 0)
        {
            _hp = 0;
            _aliveState = AliveState.Dead;
            deathEvent?.Invoke();
        }
        _battleCharacterUI.SetHP(_hp, true);
    }
    public void Health(float val)
    {
        if (_hp + val >= _maxHP)
        {
            val = _maxHP - _hp;
        }
        _battleCharacterUI.ShowChangeHPEffect(_hp, +val);
        _hp += val;
        _battleCharacterUI.SetHP(_hp, false);
    }
    public void SetUsedDamage(float val)
    {
        _usedDamage = val;
        UpdateStatsText(true);
    }
    public void SetUsedDeffence(float val)
    {
        _usedDeffence = val;
        UpdateStatsText(true);
    }
    public void SetUsedAttackSpeed(float val)
    {
        _usedAttackSpeed = val;
        UpdateStatsText(true);
    }
    public bool IsAlive()
    {
        if (_aliveState == AliveState.Alive)
        {
            return true;
        }
        return false;
    }
}
