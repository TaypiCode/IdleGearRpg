using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _iconImg;
    [SerializeField] private Image _gradeImg;
    [SerializeField] private Image _bgBtn;
    [SerializeField] private Color _normalColor, _selectedColor;
    private DungeonScriptableObject _dungeonData;
    private DungeonMenu _dungeonMenu;

    public DungeonScriptableObject DungeonData { get => _dungeonData;  }

    public void SetDungeon(DungeonScriptableObject scriptable, DungeonMenu dungeonMenu)
    {
        _dungeonData = scriptable;
        _dungeonMenu = dungeonMenu;
        _nameText.text = _dungeonData.DungeonName;
        _iconImg.sprite = _dungeonData.Sprite;
        _gradeImg.color = GradeColor.GetGradeColor(_dungeonData.Grade);
        Unselect();
    }
    public void Select()
    {
        _dungeonMenu.SelectDungeon(this);
        _bgBtn.color = _selectedColor;
       
    }
    public void Unselect()
    {
        _bgBtn.color = _normalColor;
    }
}
