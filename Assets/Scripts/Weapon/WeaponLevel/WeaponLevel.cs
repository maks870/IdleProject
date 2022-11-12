using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevel : ILevel
{
    private int levelCount;
    private int maxLevelCount;

    public bool IsMaxLevel => levelCount != maxLevelCount ? false : true;
    public WeaponLevel(int maxLevelCount)
    {
        levelCount = 0;
        this.maxLevelCount = maxLevelCount;
    }
    public void LevelUp(Action<bool> improve)
    {
        if (!IsMaxLevel)
            levelCount++;
        improve.Invoke(IsMaxLevel);
    }

    public void LevelUp(Action reward)
    {
        throw new NotImplementedException();
    }
}
