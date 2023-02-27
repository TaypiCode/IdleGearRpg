using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleUI _battleUI;
    [SerializeField] private BattleCharacter _playerCharacter;
    [SerializeField] private BattleCharacter _enemyCharacter;
    private AIScriptableObject[] _aiScriptables;
    private int _defeatedEnemyCount = 0;
    private void Start()
    {
        RemoveLootReward();
        _aiScriptables = PlayerData.aiScriptables;
        _playerCharacter.deathEvent.AddListener(PlayerDeath);
        _enemyCharacter.deathEvent.AddListener(EnemyDeath);
        _playerCharacter.SetStartStats(PlayerData.playerHP, PlayerData.playerDamage, PlayerData.playerDeffence, PlayerData.playerAttackSpeed, PlayerData.playerSkills);
        SetEnemyStat(_aiScriptables[_defeatedEnemyCount]);
        UpdateEnemyCountText();
    }
    private void SetEnemyStat(AIScriptableObject ai)
    {
        _enemyCharacter.SetStartStats(ai.Hp, ai.Damage, ai.Deffence, ai.AttackSpeed, ai.Skills);
    }
    private void PlayerDeath()
    {
        Lose();
    }
    private void EnemyDeath()
    {
        _defeatedEnemyCount++;
        UpdateEnemyCountText();
        if (_defeatedEnemyCount == _aiScriptables.Length)
        {
            Win();
        }
        else
        {
            SetEnemyStat(_aiScriptables[_defeatedEnemyCount]);
        }
    }
    private void Win()
    {
        CalculateLootReward();
        _battleUI.ShowEndGameCanvas(true);
    }
    private void Lose()
    {
        _battleUI.ShowEndGameCanvas(false);
    }
    private void UpdateEnemyCountText()
    {
        _battleUI.SetEnemyCountText(_defeatedEnemyCount, _aiScriptables.Length);
    }
    private void RemoveLootReward()
    {
        PlayerData.lootRewardFromDungeon = null;
        PlayerData.lootRewardCountFromDungeon = null;
    }
    private void CalculateLootReward()
    {
        RemoveLootReward();

        List<ItemScriptableObject> lootedItems = new List<ItemScriptableObject>();
        List<int> lootedItemsCount = new List<int>();

        ItemScriptableObject[] dungeonItems = PlayerData.loot;
        float[] itemDropChance = PlayerData.lootDropChance;
        int[] itemCount = PlayerData.lootDropCount;

        for (int i = 0; i < dungeonItems.Length; i++)
        {
            int randomChance = Random.Range(1, 101);

            if (itemDropChance[i] >= randomChance)
            {
                lootedItems.Add(dungeonItems[i]);
                lootedItemsCount.Add(itemCount[i]);
            }
        }

        PlayerData.lootRewardFromDungeon = lootedItems.ToArray();
        PlayerData.lootRewardCountFromDungeon = lootedItemsCount.ToArray();
    }
}
