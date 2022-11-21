using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StatLvl
{
    public float value;
    public int cost;
}

[Serializable]
public class Stat
{
    public string name;
    public List<StatLvl> levels;
}
