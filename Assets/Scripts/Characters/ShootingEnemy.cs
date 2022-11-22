using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] private int shootRadius;
    [SerializeField] private int runningAwayDistance;
    [SerializeField] private GameObject projectile;
    private float oldSpeed;
    private bool reloading = false;

    protected override void Start()
    {
        oldSpeed = speed;
        base.Start();    
    }

    protected override void Update()
    {
        base.Update();

        if (GetDistanceToTarget() <= shootRadius && !reloading)
        {
            Shoot();
        }

        if (GetDistanceToTarget() < runningAwayDistance)
            speed = -oldSpeed;
        else if (GetDistanceToTarget() < shootRadius)
            speed = 0;
        else
            speed = oldSpeed;
    }

    private void Shoot()
    {
        reloading = true;
        GameObject shoot = Instantiate(projectile, transform.position, Quaternion.identity);
        shoot.GetComponent<ShootProjectile>().Launch((target.transform.position - transform.position).normalized);
        StartCoroutine(Recharge(actionCooldown));
    }
    
    private IEnumerator Recharge(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
    }
}