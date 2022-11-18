using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy, IShooting
{
    [SerializeField] private int shootRaduis;
    [SerializeField] private int stopDistance;
    [SerializeField] private int runningAwayDistance;
    [SerializeField] private GameObject projectile;
    private bool isStopped = false;
    public Projectile GetProjectile() => projectile.GetComponent<Projectile>();
    protected override void Update()
    {
        if ((target.transform.position - transform.position).magnitude > stopDistance)
        {
            Move();
        }
        else if ((target.transform.position - transform.position).magnitude < runningAwayDistance)
        {
            RunningAway();
        }
        else
        { 
        
        }
    }
    protected override void Action()
    {
        Shoot();
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
        if (distanse <= shootRaduis)
        {
            GameObject shoot = Instantiate(projectile, transform);
            shoot.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed, ForceMode2D.Impulse);
        }
    }



}