using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLootRewardShower : MonoBehaviour
{
    [SerializeField] private float _delayBetweenReward;
    [SerializeField] private Transform _lootContent;
    [SerializeField] private GameObject _lootRewardPrefub;
    private Timer _timer;
    private ItemScriptableObject[] _loot;
    private int[] _lootCount;
    private int _idForShow;
    private bool _working = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (_working)
        {
            if (_timer.GetTime() <= 0)
            {
                ShowItem(_idForShow);
            }
        }
    }
    public void StartShowLoot()
    {
        _loot = PlayerData.lootRewardFromDungeon;
        _lootCount = PlayerData.lootRewardCountFromDungeon;
        ShowItem(0);
    }
    private void ShowItem(int id)
    {
        StartTimer();

        if (id < _loot.Length)
        {
            _working = true;
            Instantiate(_lootRewardPrefub, _lootContent).GetComponentInChildren<Item>().Set(_loot[id], Item.InventoryType.OnlyShow, _loot[id].StartGrade, 0, _lootCount[id], false);
        }
        else
        {
            _working = false;
        }

        _idForShow = id + 1;
    }
    private void StartTimer()
    {
        if(_timer == null)
        {
            _timer = gameObject.AddComponent<Timer>();
        }
        _timer.SetTimer(_delayBetweenReward);
    }
}
