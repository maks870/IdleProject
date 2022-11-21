using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float timer = 1;
    [SerializeField] private GameObject explosion;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    [HideInInspector] public ExplosiveBehavior explosiveBehavior;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
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
        Instantiate(explosion, transform);
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(damage);
        }
        explosiveBehavior.AddToPull(gameObject);
    }
    IEnumerator TimerToExplode()
    {
        animator.SetTrigger("Activate"); //ÒÐÈÃÃÅÐ ÀÍÈÌÀÖÈÈ ÂÇÐÛÂÀ
        yield return new WaitForSeconds(timer);
        Explode();
    }
}
