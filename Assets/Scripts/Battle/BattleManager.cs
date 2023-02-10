using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private BattleCharacter _playerCharacter;
    [SerializeField] private BattleCharacter _enemyCharacter;
    private int _defeatedEnemyCount = 0;
    private int _enemyCountInBattle;
    private int _enemyLevel;
    private void Start()
    {
        _enemyCountInBattle = PlayerData.enemyCountInBattle;
        _enemyLevel = PlayerData.enemyLevelInBattle;
        _playerCharacter.deathEvent.AddListener(PlayerDeath);
        _enemyCharacter.deathEvent.AddListener(EnemyDeath);
        _playerCharacter.SetStartStats(PlayerData.playerHP, PlayerData.playerDamage, PlayerData.playerDeffence, PlayerData.playerAttackSpeed);
        SetEnemyStat();
        UpdateEnemyCountText();
    }
    private void SetEnemyStat()
    {
        _enemyCharacter.SetStartStats(EnemyStat.GetHP(_enemyLevel), EnemyStat.GetDamage(_enemyLevel), EnemyStat.GetDeffence(_enemyLevel), EnemyStat.GetAttackSpeed(_enemyLevel));
    }
    private void PlayerDeath()
    {
        Lose();
    }
    private void EnemyDeath()
    {
        _defeatedEnemyCount++;
        UpdateEnemyCountText();
        if (_defeatedEnemyCount == _enemyCountInBattle)
        {
            Win();
        }
        else
        {
            SetEnemyStat();
        }
    }
    private void Win()
    {
        _battleUI.ShowEndGameCanvas(true);
    }
    private void Lose()
    {
        _battleUI.ShowEndGameCanvas(false);
    }
    private void UpdateEnemyCountText()
    {
        _battleUI.SetEnemyCountText(_defeatedEnemyCount, _enemyCountInBattle);
    }
}
