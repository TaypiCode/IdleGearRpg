using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static float playerHP;
    public static float playerDamage;
    public static float playerDeffence;
    public static float playerAttackSpeed;
    public static SkillScriptableObject[] playerSkills;
    public static AIScriptableObject[] aiScriptables;
    public static ItemScriptableObject[] loot;
    public static float[] lootDropChance;
    public static int[] lootDropCount;
    public static ItemScriptableObject[] lootRewardFromDungeon;
    public static int[] lootRewardCountFromDungeon;
}
