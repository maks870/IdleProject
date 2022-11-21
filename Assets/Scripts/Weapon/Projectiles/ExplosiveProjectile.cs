using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float timer = 1;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    public ExplosiveBehavior explosiveBehavior;
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
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(damage);
        }
        explosiveBehavior.AddToPull(gameObject);
    }
    IEnumerator TimerToExplode()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }
}
