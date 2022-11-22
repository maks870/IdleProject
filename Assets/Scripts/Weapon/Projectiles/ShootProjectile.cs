using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : Projectile
{
    public float speed;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void Launch(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
