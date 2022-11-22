using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float timer = 1;
    [SerializeField] private GameObject explosion;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    private bool isActivated = false;
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
        if (enemyList.Count == 1 && !isActivated)
        {
            isActivated = true;
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
    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(damage);
        }
        explosiveBehavior.AddToPull(gameObject);
        isActivated = false;
        animator.SetBool("Activated", isActivated);
    }
    IEnumerator TimerToExplode()
    {
        animator.SetBool("Activated", isActivated);
        yield return new WaitForSeconds(timer);
        Explode();
    }
}
