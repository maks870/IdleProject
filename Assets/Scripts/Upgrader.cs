using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public List<Stat> stats = new List<Stat>();
    IUpgradeble upgradeble;
    public int GetDataVariable(string statName, Dictionary<string, int> data)
    {
        int statLvl = data[statName];
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                return stat.values[statLvl];
            }
        }
        return 0;
    }
    public void Upgrade(string statName)
    {
        upgradeble = GetComponent<IUpgradeble>();
        Debug.Log(upgradeble);
        upgradeble.Upgrade(statName);
    }
    public int GetStatCost(string statName, int statLvl)
    {
        int cost = 0;
        foreach (Stat stat in stats)
        {
            if (stat.name == statName)
            {
                cost = stat.upgradeCost[statLvl];
            }
        }
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
                return stat.maxUpgradeLvl;
            }
        }
        return 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade("shootDamage");
            Upgrade("shootRange");
        }
    }
  
}
