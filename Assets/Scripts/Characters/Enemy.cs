using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [SerializeField] protected int damage;
    [SerializeField] protected Coin coin;
    [SerializeField] protected ExpPoint expPoint;
    [SerializeField] protected float actionCooldown;
    protected GameObject target;
    protected Player player;
    public readonly UnityEvent deathEvent = new UnityEvent();
    public float Speed { get { return speed; } set { speed = value; } }

    public void SetTarget(GameObject value)
    {
        target = value;
    }
    protected override void Update()
    {
        base.Update();
        moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ActionDelay());
    }

    protected virtual void Action()
    {


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            player = collision.GetComponent<Player>();
            Damage();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            player = null;
    }

    private void Damage()
    {
        if (player != null)
        {
            player.TakeDamage(damage);
            StartCoroutine(ReloadDamage());
        }
    }

    private IEnumerator ReloadDamage()
    {
        yield return new WaitForSeconds(0.5f);
        Damage();
    }

    protected override void Dead()
    {
        base.Dead();
        //Spawner.KillEnemy(this);
        ExperienceCollector.instance.Drop(expPoint, transform.position);
        CoinCollector.instance.Drop(coin, transform.position);
        deathEvent.Invoke();

    }
    IEnumerator ActionDelay()
    {
        while (true)
        {
            Action();
            yield return new WaitForSeconds(actionCooldown);
        }
    }
}
