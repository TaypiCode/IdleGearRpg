using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeItemScriptable", menuName = "ScriptableObjects/Items/UpgradeItem", order = 1)]
public class UpgradeItemScriptable : ItemScriptableObject
{
    [Header("Only 1 max count in stock !important")]
    [SerializeField] private int[] _needExpForUpgradeByGrade;
    [SerializeField] private int[] _giveExpByGrade;

    public int[] NeedExpForUpgradeByGrade { get => _needExpForUpgradeByGrade;  }
    public int[] GiveExpByGrade { get => _giveExpByGrade;  }
}
