using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartBattle()
    {
        PlayerData.enemyCountInBattle = 5;
        PlayerData.enemyLevelInBattle = 2;
        SceneManager.LoadScene(1);
    }
}
