using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SlayWeapon : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetDataVariables()
    {
        Debug.Log("установка начальных значений");
        Upgrader upgrader = GetComponent<Upgrader>();
    }
    public override void Upgrade(string statName)
    {
        YandexGame.savesData.explosiveWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.explosiveWeapon[statName];
    }
}
