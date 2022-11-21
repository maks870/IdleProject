using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SlayBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float spawnDistanse = 2;
    private bool isSlaying = false;
    public bool IsSlaying => isSlaying;

    private void Update()
    {
        Debug.Log(Player.instance.GetMoveDirection);
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
        Vector3 dir = Player.instance.GetMoveDirection;
        if (dir != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(projectile, transform.position + dir * spawnDistanse, rotation);
        }
    }
    public override void Improve(bool isMaxLevel)
    {
        //метод улучщения оружия
    }
    public override void SetDataVariables()
    {
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
