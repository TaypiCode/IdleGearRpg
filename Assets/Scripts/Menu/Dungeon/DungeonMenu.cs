using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonMenu : MonoBehaviour
{
    [SerializeField] private SaveGame _saveGame;
    [SerializeField] private GameObject _dungeonPrefub;
    [SerializeField] private Transform _dungeonListContent;
    [SerializeField] private GameObject _dungeonCanvas;
    [SerializeField] private TextMeshProUGUI _dungeonNameText;
    [SerializeField] private TextMeshProUGUI _dungeonDescriptionText;
    [SerializeField] private Transform _dungeonLootContent;
    [SerializeField] private GameObject _dungeonLootItemPrefub;
    [SerializeField] private List<DungeonScriptableObject> _dungeonScriptables= new List<DungeonScriptableObject>();
    private List<Dungeon> _dungeons = new List<Dungeon>();
    private List<DungeonLootItem> _dungeonLootItems = new List<DungeonLootItem>();
    private Dungeon _selectedDungeon;
    private void Start()
    {
        CreateDungeonList();
    }
    public void ShowDungeons()
    {
        _dungeonCanvas.SetActive(true);
        if (_dungeons.Count > 0)
        {
            _dungeons[PlayerData.lastSelectedDungeonId].Select();
        }
        else
        {
            CreateDungeonList();
        }
    }
    private void CreateDungeonList()
    {
        ClearDungeonList();
        for (int i = 0; i < _dungeonScriptables.Count; i++)
        {
            Dungeon dungeon = Instantiate(_dungeonPrefub, _dungeonListContent).GetComponent<Dungeon>();
            _dungeons.Add(dungeon);
            dungeon.SetDungeon(_dungeonScriptables[i], this);
        }
    }
    private void ClearDungeonList()
    {
        for(int i=0; i < _dungeons.Count; i++)
        {
            Destroy(_dungeons[i].gameObject);
        }
        _dungeons.Clear();
    }
    public void SelectDungeon(Dungeon dungeon)
    {
        int newId = _dungeons.IndexOf(dungeon);
        if(newId != PlayerData.lastSelectedDungeonId)
        {
            _dungeons[PlayerData.lastSelectedDungeonId].Unselect();
            PlayerData.lastSelectedDungeonId = newId;
        }
        _selectedDungeon = dungeon;
        ShowDungeonInfo();
    }
    private void ShowDungeonInfo()
    {
        DungeonScriptableObject dungeonData = _selectedDungeon.DungeonData;

        _dungeonNameText.text = dungeonData.DungeonName;
        _dungeonDescriptionText.text = dungeonData.DungeonDescription;

        CreateDungeonLootInfo();
    }
    private void CreateDungeonLootInfo()
    {
        ClearDungeonLootInfo();

        DungeonScriptableObject dungeonData = _selectedDungeon.DungeonData;

        for(int i = 0; i < dungeonData.Loot.Length; i++)
        {
            DungeonLootItem item = Instantiate(_dungeonLootItemPrefub, _dungeonLootContent).GetComponent<DungeonLootItem>();
            item.Set(dungeonData.Loot[i], dungeonData.LootDropCount[i], dungeonData.LootDropChance[i]);
            _dungeonLootItems.Add(item);
        }
    }
    private void ClearDungeonLootInfo()
    {
        for(int i = 0; i < _dungeonLootItems.Count; i++)
        {
            Destroy(_dungeonLootItems[i].gameObject);
        }
        _dungeonLootItems.Clear();
    }
    public void StartDungeon()
    {
        DungeonScriptableObject dungeonData = _selectedDungeon.DungeonData;
        PlayerData.aiScriptables = dungeonData.Enemies;
        PlayerData.loot = dungeonData.Loot;
        PlayerData.lootDropChance = dungeonData.LootDropChance;
        PlayerData.lootDropCount = dungeonData.LootDropCount;
        FindObjectOfType<UpgradeItems>().UndressItems();
        _saveGame.SaveProgress();
        SceneManager.LoadScene(1);
    }
}
