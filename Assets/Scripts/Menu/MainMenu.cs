using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveGame _saveGame;
    public void StartBattle(DungeonScriptableObject dungeon)
    {
        PlayerData.aiScriptables = dungeon.Enemies;
        PlayerData.loot = dungeon.Loot;
        PlayerData.lootDropChance = dungeon.LootDropChance;
        PlayerData.lootDropCount = dungeon.LootDropCount;
        _saveGame.SaveProgress();
        SceneManager.LoadScene(1);
    }
}
