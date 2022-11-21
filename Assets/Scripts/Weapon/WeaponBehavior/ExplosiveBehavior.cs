using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Upgrader))]
public class ExplosiveBehavior : Behavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float radius;
    [SerializeField] private int maxBobmObj = 20;
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
            if (Mathf.Pow(randomX, 2) + Mathf.Pow(randomY, 2) <= Mathf.Pow(radius, 2))
            {               
                dropPlace.x = randomX;
                dropPlace.y = randomY;
                dropPlace.z = 0;
                break;
            }
        }

        if (invisiblePull.Count > 0)
        {
            RemoveFromPull().transform.position = transform.position + dropPlace;
        }
        else
        {
            GameObject bombObject = visiblePull[0];
            visiblePull.Remove(bombObject);
            visiblePull.Add(bombObject);
            bombObject.transform.position = transform.position + dropPlace;
        }
        Instantiate(projectile, transform.position + dropPlace, Quaternion.identity);
    }
    private void AddToPull(GameObject projectile)
    {
        invisiblePull.Add(projectile);
        visiblePull.Remove(projectile);
        projectile.SetActive(false);
    }
    private GameObject RemoveFromPull()
    {
        GameObject bombObject = invisiblePull[0];
        invisiblePull.Remove(bombObject);
        visiblePull.Add(bombObject);
        bombObject.SetActive(true);
        return bombObject;
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
        Debug.Log("установка начальных значений");
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
