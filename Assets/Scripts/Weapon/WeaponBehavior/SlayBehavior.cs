using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SlayBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private float spawnDistanse = 2;
    private int countSpawnProjectilesCount = 0;
    private bool isReady = false;
    private float angleRadius = 90;
    private Vector3 dir;


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
        countSpawnProjectilesCount++;
    }
    public override void Use()
    {
        dir = Player.instance.GetMoveDirection;
        if (dir == Vector3.zero) return;

        int anglePartsCount = countSpawnProjectilesCount + 1;
        float angleOffset = angleRadius / anglePartsCount;
        float angle;

        for (int i = 1; i < anglePartsCount; i++)
        {
            angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward) + (angleRadius / 2) - (i * angleOffset);

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject newProjectile = Instantiate(projectileObj, transform.position + dir * spawnDistanse, rotation);

            Vector3 dirProjectile = rotation * Vector3.up;
            newProjectile.GetComponent<SlayProjectile>().Launch(dirProjectile);
        }
    }
    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
            countSpawnProjectilesCount++;
    }
    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        SlayProjectile projectile = projectileObj.GetComponent<SlayProjectile>();
        projectile.damage = (int)(projectile.damage * upgrader.GetDataVariable("slayDamage", YandexGame.savesData.slayWeapon) / 100);
        projectile.speed *= upgrader.GetDataVariable("slaySpeed", YandexGame.savesData.slayWeapon) / 100;
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
