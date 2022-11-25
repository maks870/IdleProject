using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Upgrader))]
public class ExplosiveBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private float radius;
    [SerializeField] private float radiusImprove;
    [SerializeField] private int maxBobmObj = 20;
    private float bombRadius;
    private List<ExplosiveProjectile> invisiblePull = new List<ExplosiveProjectile>();
    private List<ExplosiveProjectile> visiblePull = new List<ExplosiveProjectile>();
    private void Awake()
    {
        SetDataVariables();
    }

    private void DropProjectile()
    {
        bool findPlace = false;
        Vector3 dropPlace = new Vector3();

        while (!findPlace)
        {
            float x = Mathf.Sqrt(Mathf.Pow(radius, 2) / 2);
            float randomX = Random.Range(-x, x);
            float y = x - Mathf.Abs(randomX);
            float randomY = Random.Range(-y, y);
            dropPlace.x = randomX;
            dropPlace.y = randomY;
            dropPlace.z = 0;

            if (CheckPlace(dropPlace))
                break;
        }

        if (invisiblePull.Count > 0)
        {
            RemoveFromPull().transform.position = transform.position + dropPlace;
        }
        else
        {
            ExplosiveProjectile bombObject = visiblePull[0];
            visiblePull.Remove(bombObject);
            bombObject?.Explode();
            bombObject.transform.position = transform.position + dropPlace;
        }
    }

    private bool CheckPlace(Vector3 dropPlace)
    {
        float range;
        for (int i = 0; i < visiblePull.Count; i++)
        {
            if (visiblePull[i] == null)
                continue;
            range = (visiblePull[i].transform.position - (dropPlace + transform.position)).magnitude;

            if (range <= bombRadius)
                return false;
        }
        return true;
    }

    private void CreatePull()
    {
        for (int i = 0; i < maxBobmObj; i++)
        {
            ExplosiveProjectile newProjectileObject = Instantiate(projectileObj, transform.position, Quaternion.identity).GetComponent<ExplosiveProjectile>();
            newProjectileObject.explosiveBehavior = this;
            AddToPull(newProjectileObject);
        }
    }

    private ExplosiveProjectile RemoveFromPull()
    {
        ExplosiveProjectile bombObject = invisiblePull[0];
        invisiblePull.Remove(bombObject);
        visiblePull.Add(bombObject);
        bombObject.gameObject.SetActive(true);
        return bombObject;
    }

    public void AddToPull(ExplosiveProjectile projectile)
    {
        invisiblePull.Add(projectile);
        visiblePull.Remove(projectile);
        projectile.gameObject.SetActive(false);
    }

    public override void Combine()
    {
        //логика объединения оружия
    }

    public override void ActiveBehavior()
    {
        bombRadius = projectileObj.GetComponent<CircleCollider2D>().radius;
        CreatePull();
    }

    public override void Use()
    {
        DropProjectile();
    }

    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
            radius += radiusImprove;
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        ExplosiveProjectile projectile = projectileObj.GetComponent<ExplosiveProjectile>();

        float explodeDamage = (int)(upgrader.GetBaseValue("explodeDamage") * upgrader.GetDataVariable("explodeDamage", YandexGame.savesData.explosiveWeapon) / 100);
        projectile.damage = (int)(projectile.damage * explodeDamage);

        float explodeRadius = (int)(upgrader.GetBaseValue("explodeRadius") * upgrader.GetDataVariable("explodeRadius", YandexGame.savesData.explosiveWeapon) / 100);

        projectileObj.GetComponent<CircleCollider2D>().radius = explodeRadius;
        projectile.newSize = explodeRadius;
    }
    public void Upgrade(string statName)
    {
        YandexGame.savesData.explosiveWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.explosiveWeapon[statName];
    }
}
