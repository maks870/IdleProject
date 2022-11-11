using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelUpImprove();
public interface ILevel
{
    public void LevelUp(LevelUpImprove Improve);

}
