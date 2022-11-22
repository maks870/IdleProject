using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinProjectile : Projectile
{
    public float speed;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
