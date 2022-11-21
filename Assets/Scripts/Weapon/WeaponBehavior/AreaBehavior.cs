using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Upgrader))]
public class AreaBehavior : Behavior, IUpgradeble
{
    [SerializeField] private int damage;
    [SerializeField] private int slow;
    [SerializeField] private float areaSize;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Speed *= (100f - slow)/100f;
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
        Debug.Log("Область нанесения урона улучшена");
    }
    public override void ActiveBehavior()
    {
        spriteRenderer.enabled = true;
        SetAreaSize(areaSize);
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
    public void SetAreaSize(float newAreaSize)
    {
        areaSize = newAreaSize;
        transform.localScale = Vector3.one * areaSize;
    }

    public override void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        damage = (int)upgrader.GetDataVariable("areaDamage", YandexGame.savesData.areaWeapon);
        slow = (int)upgrader.GetDataVariable("areaSlow", YandexGame.savesData.areaWeapon);

    }
    public override void Upgrade(string statName)
    {
        YandexGame.savesData.areaWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.areaWeapon[statName];
    }
}
