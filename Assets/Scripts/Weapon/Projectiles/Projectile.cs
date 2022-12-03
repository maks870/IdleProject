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
            Destroy(gameObject);
        }
    }

    public abstract void Dead();
}
