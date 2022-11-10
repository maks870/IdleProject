using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShootBehavior : WeaponBehavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private CircleCollider2D shootZone;
    private List<Enemy> enemyList = new List<Enemy>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
        }
    }
    public override void CombineWeapon()
    {
        //логика объединения оружия
    }

    public override void UseBehavior()
    {
        if (enemyList.Count == 0)
        {
            return;
        }
        Enemy targetEnemy = enemyList[0];
        float distanse = (transform.position - targetEnemy.transform.position).magnitude;
        foreach (Enemy enemy in enemyList)
        {
            float newDistanse = (transform.position - enemy.transform.position).magnitude;
            if (newDistanse <= distanse)
            {
                targetEnemy = enemy;
                distanse = newDistanse;
            }
        }
        GameObject shoot = Instantiate(projectile, transform);
        shoot.GetComponent<Rigidbody2D>().AddForce((targetEnemy.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed , ForceMode2D.Impulse);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
