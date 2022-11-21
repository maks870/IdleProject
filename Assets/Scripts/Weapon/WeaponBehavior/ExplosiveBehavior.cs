using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Upgrader))]
public class ExplosiveBehavior : Behavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float radius;
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
            if (Mathf.Pow(randomX, 2) + Mathf.Pow(randomY, 2) <= Mathf.Pow(radius, 2))
            {
                dropPlace.x = randomX;
                dropPlace.y = randomY;
                dropPlace.z = 0;
                break;
            }
        }
        Instantiate(projectile, transform.position + dropPlace, Quaternion.identity);
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
        DropProjectile();
    }
    public override void Improve(bool isMaxLevel)
    {
        //метод улучщения оружия
    }

    // Start is called before the first frame update
    public override void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        projectile.GetComponent<Projectile>().damage = (int)upgrader.GetDataVariable("explodeDamage", YandexGame.savesData.explosiveWeapon);
        projectile.GetComponent<CircleCollider2D>().radius = upgrader.GetDataVariable("explodeRadius", YandexGame.savesData.explosiveWeapon);
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
