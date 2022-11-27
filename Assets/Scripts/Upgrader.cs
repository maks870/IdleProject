using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public List<Stat> stats = new List<Stat>();
    IUpgradeble upgradeble;
    public void Upgrade(string statName)
    {
        upgradeble = GetComponent<IUpgradeble>();
        upgradeble.Upgrade(statName);
    }
    private int GetStatCost(string statName, int statLvl)
    {
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                return stat.levels[statLvl].cost;
            }
        }
        return 0;
    }

    public int GetStatCost(string statName)
    {
        int cost = GetStatCost(statName, GetStatLvl(statName));
        return cost;
    }

    public int GetStatLvl(string statName)
    {
        upgradeble = GetComponent<IUpgradeble>();
        return upgradeble.GetStatLvl(statName);
    }
    public int GetMaxStatLvl(string statName)
    {
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                return stat.levels.Count - 1;
            }
        }
        return 0;
    }
    public float GetValue(string statName)
    {
        int statLvl = GetStatLvl(statName);
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                return stat.levels[statLvl].value;
            }
        }
        return 0;
    }
    public float GetBaseValue(string statName)
    {
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                return stat.baseValue;
            }
        }
        return 0;
    }
    public float GetValueUpgrade(string statName)
    {
        int statLvl = GetStatLvl(statName);
        float currentValue;
        float nextValue;
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                currentValue = stat.levels[statLvl].value;
                if (currentValue == stat.levels[stat.levels.Count - 1].value)
                {
                    return 0;
                }
                nextValue = stat.levels[statLvl + 1].value;
                return nextValue - currentValue;
            }
        }
        return 0;
    }
}
