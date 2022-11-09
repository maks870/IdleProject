using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShootBehavior : MonoBehaviour, IWeaponBehavior
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
    public void CombineWeapon()
    {
        //логика объединения оружия
    }

    public void UseBehavior()
    {   if (enemyList.Count == 0)
        {
            return;
        }
        float distanse = 0;
        Enemy targetEnemy = enemyList[0];
        foreach (Enemy enemy in enemyList)
        {
            float newDistanse = (transform.position - enemy.transform.position).magnitude;
            if (newDistanse < distanse)
            {
                targetEnemy = enemy;
            }
        }
        GameObject shoot = Instantiate(projectile);
        shoot.GetComponent<Rigidbody2D>().AddForce(targetEnemy.transform.position - transform.position);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
