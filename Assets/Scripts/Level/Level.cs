using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ILevel
{
    private int levelCount;
    private int maxLevelCount;

    public bool IsMaxLevel => levelCount != maxLevelCount ? false : true;
    public Level(int maxLevelCount)
    {
        levelCount = 1;
        this.maxLevelCount = maxLevelCount;
    }
    public void LevelUp(Action<bool> improve)
    {
        improve.Invoke(IsMaxLevel);
        if (!IsMaxLevel)
            levelCount++;
    }

    public void LevelUp(Action reward)
    {
        if (!IsMaxLevel)
            levelCount++;
        reward.Invoke();
    }
}
