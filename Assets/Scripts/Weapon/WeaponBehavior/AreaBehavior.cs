using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Upgrader))]
public class AreaBehavior : Behavior, IUpgradeble
{
    [SerializeField] private int damage;
    [SerializeField] private float slow;
    [SerializeField] private float areaSize = 1;
    [SerializeField] private float areaImprove = 0.25f;
    [SerializeField] private GameObject spriteObject;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;


    private void Awake()
    {
        SetDataVariables();
    }

    private void Start()
    {
        spriteObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.speedMultiplier *= (100f - slow) / 100f;
            enemyList.Add(enemy);
            enemy.deathEvent.AddListener(delegate { enemyList.Remove(enemy); });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.speedMultiplier /= (100f - slow) / 100f;
            enemyList.Remove(enemy);
            enemy.deathEvent.RemoveListener(delegate { enemyList.Remove(enemy); });
        }
    }

    public override void Combine()
    {
        //логика объединения оружия
    }

    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
        {
            areaSize += areaImprove;
            SetAreaSize();
        }
    }

    public override void ActiveBehavior()
    {
        spriteObject.SetActive(true);
        SetAreaSize();
        animator = GetComponent<Animator>();
    }

    public override void Use()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i]?.TakeDamage(damage);
        }
        animator.SetTrigger("Use");
    }

    public void SetAreaSize()
    {
        transform.localScale = Vector3.one * areaSize;
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        damage = (int)(upgrader.GetBaseValue("areaDamage") * upgrader.GetValue("areaDamage") / 100);
        slow = upgrader.GetBaseValue("areaSlow") * upgrader.GetValue("areaSlow");
    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.areaWeapon.AddValue(statName, 1);
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.areaWeapon.GetValue(statName);
    }
}
