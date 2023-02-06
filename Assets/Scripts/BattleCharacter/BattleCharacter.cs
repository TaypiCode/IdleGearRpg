﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] private BattleCharacterUI _battleCharacterUI;
    [SerializeField] private BattleCharacter _enemy;
    private float _hp, _maxHP;
    private float _damage;
    private float _deffence;
    private float _attackSpeed; //attacksPerMinute
    private AliveState _aliveState;
    private enum AliveState
    {
        Alive,
        Dead
    }

    private Timer _attackTimer;
    private void Start()
    {
        _aliveState = AliveState.Alive;
        //get static data from menu
        _maxHP = 5;
        _hp = _maxHP;
        _damage = 1;
        _deffence = 0;
        _attackSpeed = 60;

        _battleCharacterUI.SetStartValues(_hp, _maxHP, _damage, _deffence, _attackSpeed);

        _attackTimer = this.gameObject.AddComponent<Timer>();
        CooldownSimpleAttack();
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
        _hp -= val;
        if (_hp <= 0)
        {
            _hp = 0;
            _aliveState = AliveState.Dead;
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
