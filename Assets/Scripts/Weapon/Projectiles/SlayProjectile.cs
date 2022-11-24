using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayProjectile : Projectile
{
    public float speed;
    private void Start()
    {
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    public void Launch(Vector3 dir)
    {
        GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
    }
}
