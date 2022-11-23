using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Upgrader))]
public class ExplosiveBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float radius;
    [SerializeField] private float radiusImprove;
    [SerializeField] private int maxBobmObj = 20;
    private float bombRadius;
    private List<GameObject> invisiblePull = new List<GameObject>();
    private List<GameObject> visiblePull = new List<GameObject>();
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
            GameObject bombObject = visiblePull[0];

            visiblePull.Remove(bombObject);
            bombObject.GetComponent<ExplosiveProjectile>().Explode();
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
            GameObject newProjectileObject = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectileObject.GetComponent<ExplosiveProjectile>().explosiveBehavior = this;
            AddToPull(newProjectileObject);
        }
    }
    private GameObject RemoveFromPull()
    {
        GameObject bombObject = invisiblePull[0];
        invisiblePull.Remove(bombObject);
        visiblePull.Add(bombObject);
        bombObject.SetActive(true);
        return bombObject;
    }
    public void AddToPull(GameObject projectile)
    {
        invisiblePull.Add(projectile);
        visiblePull.Remove(projectile);
        projectile.SetActive(false);
    }
    public override void Combine()
    {
        //логика объединения оружия
    }
    public override void ActiveBehavior()
    {
        bombRadius = projectile.GetComponent<CircleCollider2D>().radius;
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

    // Start is called before the first frame update
    public override void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        projectile.GetComponent<Projectile>().damage = (int)upgrader.GetDataVariable("explodeDamage", YandexGame.savesData.explosiveWeapon);
        projectile.GetComponent<CircleCollider2D>().radius = upgrader.GetDataVariable("explodeRadius", YandexGame.savesData.explosiveWeapon);
        projectile.GetComponent<ExplosiveProjectile>().newSize = upgrader.GetDataVariable("explodeRadius", YandexGame.savesData.explosiveWeapon);
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
