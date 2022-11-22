using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy, IShooting
{
    [SerializeField] private int shootRadius;
    [SerializeField] private int runningAwayDistance;
    [SerializeField] private GameObject projectile;
    private bool isShooting = false;
    public Projectile GetProjectile() => projectile.GetComponent<Projectile>();

    protected override void Update()
    {
        if ((target.transform.position - transform.position).magnitude > shootRadius)
        {
            Move();
            isShooting = false;
        }
        else
        {
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(ShootingDelay());
            }
        }

        if ((target.transform.position - transform.position).magnitude < runningAwayDistance)
            RunningAway();
        else
            Stop();

    }
    private void Move()
    {
        base.Update();
    }
    private void RunningAway()
    {
        rb.velocity = -moveDirection * speed;
        moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
    }
    private void Stop()
    {
        rb.velocity = Vector2.zero;
        moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
    }

    public void Shoot()
    {
        float distanse = (transform.position - target.transform.position).magnitude;
        if (distanse <= shootRadius)
        {
            GameObject shoot = Instantiate(projectile, transform);
            shoot.transform.parent = null;
            shoot.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed, ForceMode2D.Impulse);
        }
    }
    IEnumerator ShootingDelay()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(actionCooldown);
        }
    }


}