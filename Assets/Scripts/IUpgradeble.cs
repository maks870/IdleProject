using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeble
{
    public int GetStatLvl(string statName);
    public void Upgrade(string statName);
    public void SetDataVariables();


}
