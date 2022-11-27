using UnityEngine;
using YG;

public class SlayBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private float spawnDistanse = 2;
    private int countSpawnProjectilesCount = 0;
    private float angleRadius = 90;
    private Vector3 dir;
    private void Awake()
    {
        SetDataVariables();
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
        projectile.damage = (int)(upgrader.GetBaseValue("slayDamage") * upgrader.GetValue("slayDamage") / 100);
        projectile.speed = upgrader.GetBaseValue("slaySpeed") * upgrader.GetValue("slaySpeed") / 100;
    }
    public void Upgrade(string statName)
    {
        YandexGame.savesData.slayWeapon.AddValue(statName, 1);
    }
    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.slayWeapon.GetValue(statName);
    }

    public override void Combine()
    {
    }
}
