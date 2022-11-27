using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Upgrader))]
public class ShootBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private float radius;
    [SerializeField] private CircleCollider2D shootZone;
    private List<Enemy> enemyList = new List<Enemy>();
    private int targetCount = 1;
    private bool isAddTarget = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.deathEvent.AddListener(delegate { enemyList.Remove(enemy); });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Remove(enemy);
            enemy.deathEvent.RemoveListener(delegate { enemyList.Remove(enemy); });
        }
    }

    private void Awake()
    {
        SetDataVariables();
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
        if (isAddTarget)
        {
            targetCount++;
            isAddTarget = false;
        }

        if (enemyList.Count == 0)
        {
            return;
        }
        List<Vector3> directionList = new List<Vector3>();

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] != null)
            {
                directionList.Add(enemyList[i].transform.position - transform.position);
            }
        }

        directionList.Sort(delegate (Vector3 x, Vector3 y)
        {
            float firstDist = x.magnitude;
            float secondDist = y.magnitude;
            return firstDist.CompareTo(secondDist);
        });

        //enemyList.Sort(delegate (Enemy x, Enemy y)
        //{
        //    Vector3 xTrans = x.transform.position;
        //    Vector3 yTrans = y.transform.position;
        //    float firstDist = (transform.position - xTrans).magnitude;
        //    float secondDist = (transform.position - yTrans).magnitude;
        //    return firstDist.CompareTo(secondDist);
        //});

        int counterMax = targetCount <= directionList.Count ? targetCount : directionList.Count;

        for (int i = 0; i < counterMax; i++)
        {
            GameObject shoot = Instantiate(projectileObj, transform);
            shoot.GetComponent<ShootProjectile>().Launch(directionList[i].normalized);
        }
    }

    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
            isAddTarget = true;

    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.damage = (int)(upgrader.GetBaseValue("shootDamage") * upgrader.GetValue("shootDamage") / 100);
        radius = upgrader.GetBaseValue("shootRange") * upgrader.GetValue("shootRange") / 100;
        shootZone.radius = radius;

    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.shootWeapon.AddValue(statName, 1);
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.shootWeapon.GetValue(statName);
    }
}
