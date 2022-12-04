using UnityEngine;

public class ShootProjectile : Projectile
{
    public float speed;
    private bool isMove = false;
    private Vector3 moveDirection;
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
        isMove = true;
        moveDirection = direction;
    }

    private void FixedUpdate()
    {
        if(isMove)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, speed / 50);
    }

    public override void Dead()
    {
        Destroy(gameObject);
    }
}
