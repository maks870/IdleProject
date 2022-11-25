using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float timer = 1;
    [SerializeField] private float explodeRadius;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform explodeArea;
    private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;
    private bool isActivated = false;
    [HideInInspector] public ExplosiveBehavior explosiveBehavior;
    [HideInInspector] public float newSize;

    public float ExplodeRadius => explodeRadius;
    private void Start()
    {
        explodeArea.localScale = new Vector3(newSize * 2, newSize * 2, 0);
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
        explosiveBehavior.AddToPull(this);
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
