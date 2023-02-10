using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyStat
{
    public static float GetHP(int level)
    {
        return level * 5;
    }
    public static float GetDamage(int level)
    {
        return level * 1;
    }
    public static float GetDeffence(int level)
    {
        return level * 5;
    }
    public static float GetAttackSpeed(int level)
    {
        return level * 20;
    }
}
