using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : ILevel
{
    private int levelCount;
    private int maxLevelCount;

    public bool IsMaxLevel => levelCount != maxLevelCount ? false : true;
    public PlayerLevel(int maxLevelCount)
    {
        levelCount = 0;
        this.maxLevelCount = maxLevelCount;
    }
    public void LevelUp(Action reward)
    {
        if (!IsMaxLevel)
            levelCount++;
        reward.Invoke();
    }

    public void LevelUp(Action<bool> improve)
    {
        throw new NotImplementedException();
    }
}
