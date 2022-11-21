using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    private float timer = 1;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Add(enemy);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (enemyList.Count == 1)
        {
            StartCoroutine(TimerToExplode());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Remove(enemy);
        }
    }
    private void Explode()
    {
        animator.SetTrigger(""); //“–»√√≈– ¿Õ»Ã¿÷»» ¬«–€¬¿
        foreach (Enemy enemy in enemyList)
        {
            enemy.TakeDamage(damage);
        }
    }
    IEnumerator TimerToExplode()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }
}
