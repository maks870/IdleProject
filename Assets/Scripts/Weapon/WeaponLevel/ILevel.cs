using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelUpImprove(bool isMaxLevel);
public interface ILevel
{
    public void LevelUp(LevelUpImprove improve);

}
