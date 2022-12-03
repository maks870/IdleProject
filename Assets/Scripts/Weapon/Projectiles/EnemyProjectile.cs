using UnityEngine;

public class EnemyProjectile : ShootProjectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Dead();
        }
    }
}
