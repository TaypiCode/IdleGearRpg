using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] private BattleCharacterUI _battleCharacterUI;
    [SerializeField] private BattleCharacter _enemy;
    private float _hp, _maxHP;
    private float _damage;
    private float _deffence;
    private float _attackSpeed; //attacksPerMinute
    private AliveState _aliveState;
    public UnityEvent deathEvent;
    private enum AliveState
    {
        Alive,
        Dead
    }

    private Timer _attackTimer;
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
    public void SetStartStats(float hp, float damage, float deffence, float attackSpeed)
    {
        _maxHP = hp;
        _hp = _maxHP;
        _damage = damage;
        _deffence = deffence;
        _attackSpeed = attackSpeed;

        _battleCharacterUI.SetStartValues(_hp, _maxHP, _damage, _deffence, _attackSpeed);

        CooldownSimpleAttack();

        _aliveState = AliveState.Alive;
    }
    private void SimpleAttack()
    {
        if (_aliveState == AliveState.Alive)
        {
            if (_enemy.IsAlive())
            {
                _enemy.GetDamage(_damage);
                CooldownSimpleAttack();
            }
        }
    }
    private void CooldownSimpleAttack()
    {
        _attackTimer.SetTimer(60 / _attackSpeed); //attacks per min
    }
    public void GetDamage(float val)
    {
        if(_deffence > 0)
        {
            if(_deffence >= 100)
            {
                val = val * 0.01f; //1% 
            }
            else
            {
                val = val - val * _deffence / 100;
            }
        }
        _battleCharacterUI.ShowDamageEffect(_hp, val);
        _hp -= val;
        if (_hp <= 0)
        {
            _hp = 0;
            _aliveState = AliveState.Dead;
            deathEvent?.Invoke();
        }
        _battleCharacterUI.SetHP(_hp, true);
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
