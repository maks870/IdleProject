using UnityEngine;

public class ShootProjectile : Projectile
{
    public float speed;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Dead();
        }
    }

    public void Launch(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public override void Dead()
    {
        Destroy(gameObject);
    }
}
