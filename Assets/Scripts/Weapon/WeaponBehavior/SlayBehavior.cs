using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SlayBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float spawnDistanse = 2;
    [SerializeField] private int pierceCount = 1;
    private Vector3 dir;
    private bool isReady = false;

    private void Awake()
    {
        SetDataVariables();
    }
    private void Update()
    {
    }
    public override void Combine()
    {
        //логика объединения оружия
    }

    public override void ActiveBehavior()
    {
    }
    public override void Use()
    {
        dir = Player.instance.GetMoveDirection;
        if (dir == Vector3.zero) return;

        dir = Player.instance.GetMoveDirection;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject newProjectile = projectile;
        newProjectile.GetComponent<SlayProjectile>().pierceLeft = pierceCount;
        Instantiate(newProjectile, transform.position + dir * spawnDistanse, rotation);
    }
    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
            pierceCount++;
    }
    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        projectile.GetComponent<Projectile>().damage = (int)upgrader.GetDataVariable("slayDamage", YandexGame.savesData.slayWeapon);
        projectile.GetComponent<SlayProjectile>().speed = upgrader.GetDataVariable("slaySpeed", YandexGame.savesData.slayWeapon);
    }
    public void Upgrade(string statName)
    {
        YandexGame.savesData.slayWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.slayWeapon[statName];
    }
}
