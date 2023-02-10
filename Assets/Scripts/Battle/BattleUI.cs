using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [Header("EndGame")]
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private TextMeshProUGUI _winLoseText;
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SetEnemyCountText(int currentDefeated, int maxEnemy)
    {
        _enemyCountText.text = currentDefeated + "/" + maxEnemy;
    }
    public void ShowEndGameCanvas(bool isWin) //and rewards
    {
        _endGameCanvas.SetActive(true);
        if(isWin)
        {
            _winLoseText.text = "Вы выиграли";
        }
        else
        {
            _winLoseText.text = "Вы проиграли";
        }
    }
}
