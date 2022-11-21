using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayProjectile : Projectile
{
    private List<Enemy> enemyList = new List<Enemy>();
    public SlayBehavior slayBehavior;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Add(enemy);
            Slay(enemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Remove(enemy);
        }
    }
    public void Slay(Enemy enemy)
    {
        if (enemyList.Contains(enemy))
        { 
        enemy.TakeDamage(damage);
        }
        StartCoroutine(ReloadDamage(enemy));

    }
    private IEnumerator ReloadDamage(Enemy enemy)
    {
        yield return new WaitForSeconds(0.5f);
        if (slayBehavior.IsSlaying)
        {
            Slay(enemy);
        }
    }
}
