using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayProjectile : Projectile
{
    public float speed;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Player.instance.GetMoveDirection * speed, ForceMode2D.Impulse);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
