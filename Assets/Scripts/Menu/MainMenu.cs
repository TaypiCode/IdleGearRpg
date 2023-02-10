using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartBattle()
    {
        PlayerData.playerHP = 15;
        PlayerData.playerDamage = 2;
        PlayerData.playerDeffence = 20;
        PlayerData.playerAttackSpeed = 60;
        PlayerData.enemyCountInBattle = 5;
        PlayerData.enemyLevelInBattle = 2;
        SceneManager.LoadScene(1);
    }
}
