using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILevel
{
    public void LevelUp(Action<bool> improve);
    public void LevelUp(Action reward);

}
