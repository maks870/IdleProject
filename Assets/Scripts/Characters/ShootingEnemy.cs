using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy, IShooting
{
    [SerializeField] private int shootRaduis;
    [SerializeField] private GameObject projectile;
    public Projectile GetProjectile() => projectile.GetComponent<Projectile>();

    public void Shoot()
    {
        float distanse = (transform.position - target.transform.position).magnitude;
        if (distanse <= shootRaduis)
        {
            GameObject shoot = Instantiate(projectile, transform);
            shoot.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed, ForceMode2D.Impulse);
        }
    }
    protected override void Action()
    {
        Shoot();
    }
}