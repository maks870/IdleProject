using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Projectile : MonoBehaviour
{
    public int damage;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Dead();
        }
    }

    public abstract void Dead();
}
