using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveGame _saveGame;
    public void StartBattle()
    {
        PlayerData.enemyCountInBattle = 5;
        PlayerData.enemyLevelInBattle = 2;
        _saveGame.SaveProgress();
        SceneManager.LoadScene(1);
    }
}
