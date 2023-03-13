using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DungeonScriptable", menuName = "ScriptableObjects/Dungeon", order = 1)]
public class DungeonScriptableObject : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _dungeonName;
    [SerializeField] private string _dungeonDescription;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _grade;
    [SerializeField] private AIScriptableObject[] _enemies;
    [SerializeField] private ItemScriptableObject[] _loot;
    [SerializeField] private float[] _lootDropChance;
    [SerializeField] private int[] _lootDropCount;

    public string DungeonName { get => _dungeonName; }
    public string DungeonDescription { get => _dungeonDescription; }
    public Sprite Sprite { get => _sprite;  }
    public AIScriptableObject[] Enemies { get => _enemies;  }
    public ItemScriptableObject[] Loot { get => _loot; }
    public float[] LootDropChance { get => _lootDropChance; }
    public int[] LootDropCount { get => _lootDropCount;  }
    public int Grade { get => _grade; }
}
