using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AreaBehavior : WeaponBehavior
{
    [SerializeField] private int damage;
    [SerializeField] private float areaSize;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;

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
    public override void CombineWeapon()
    {
        //логика объединения оружия
    }
    public override void ImproveWeapon(bool isMaxLevel)
    {
        //метод улучщения оружия
    }
    public override void UseBehavior()
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

    void Start()
    {
        SetAreaSize(areaSize);
        animator = GetComponent<Animator>();
    }


    void Update()
    {

    }
}
