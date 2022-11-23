using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Upgrader))]
public class AreaBehavior : Behavior, IUpgradeble
{
    [SerializeField] private int damage;
    [SerializeField] private int slow;
    [SerializeField] private float areaSize = 1;
    [SerializeField] private float areaImprove = 0.25f;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        SetDataVariables();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Speed *= (100f - slow) / 100f;
            enemyList.Add(enemy);
            enemy.deathEvent.AddListener(delegate { enemyList.Remove(enemy); });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Speed /= (100f - slow) / 100f;
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
        spriteRenderer.enabled = true;
        SetAreaSize();
        animator = GetComponent<Animator>();
    }

    public override void Use()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(damage);
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
        damage = (int)upgrader.GetDataVariable("areaDamage", YandexGame.savesData.areaWeapon);
        slow = (int)upgrader.GetDataVariable("areaSlow", YandexGame.savesData.areaWeapon);

    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.areaWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.areaWeapon[statName];
    }
}
