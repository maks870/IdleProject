using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShootBehavior : Behavior
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private CircleCollider2D shootZone;
    private List<Enemy> enemyList = new List<Enemy>();
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
    public override void Combine()
    {
        //логика объединения оружия
    }

    public override void Use()
    {
        if (enemyList.Count == 0)
        {
            return;
        }
        Enemy targetEnemy = enemyList[0];
        float distanse = (transform.position - targetEnemy.transform.position).magnitude;/////// ошибка, надо проверить target enemy на null
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
    public override void Improve(bool isMaxLevel)
    {
        Debug.Log("Стрельба улучшена");
        //метод улучщения оружия
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
