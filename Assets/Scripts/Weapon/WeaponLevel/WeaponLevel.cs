using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevel : MonoBehaviour, ILevel
{
    private int levelCount;
    private int maxLevelCount;

    public bool IsMaxLevel => levelCount != maxLevelCount ? false : true;
    public void LevelUp(LevelUpImprove Improve)
    {
        if (!IsMaxLevel)
            levelCount++;
        Improve.Invoke(IsMaxLevel);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
