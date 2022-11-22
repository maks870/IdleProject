using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy, IShooting
{
    [SerializeField] private int shootRadius;
    [SerializeField] private int runningAwayDistance;
    [SerializeField] private GameObject projectile;
    private bool shootAvaliable = false;
    private bool isShooting = false;
    public Projectile GetProjectile() => projectile.GetComponent<Projectile>();

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log((target.transform.position - transform.position).magnitude);
        }
        if ((target.transform.position - transform.position).magnitude > shootRadius)
        {
            Move();
            shootAvaliable = false;
        }
        else
        {
            if (!shootAvaliable)
            {
                shootAvaliable = true;
                if(!isShooting)
                    StartCoroutine(ShootingDelay());

            }
        }

        if ((target.transform.position - transform.position).magnitude < runningAwayDistance)
            RunningAway();
        else if ((target.transform.position - transform.position).magnitude < shootRadius)
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
        isShooting = true;
        while (shootAvaliable)
        {
            Shoot();
            yield return new WaitForSeconds(actionCooldown);
        }
        isShooting = false;
    }


}