using System.Collections;
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
    public float Speed { get { return base.speed; } set { base.speed = value; } }

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

    protected float GetDistanceToTarget()
    {
        return Vector3.Distance(target.transform.position, transform.position);
    }
    public override void TakeDeadlyDamage(bool noDrop)
    {
        if (noDrop)
        {
            deathEvent.Invoke();
            Destroy(gameObject);
        }
        else
        {
            Dead();
        }
    }

    protected override void Dead()
    {
        base.Dead();
        deathEvent.AddListener(delegate { Destroy(gameObject); });
        ExperienceCollector.instance?.Drop(expPoint, transform.position);
        CoinCollector.instance.Drop(coin, transform.position);
        deathEvent.Invoke();
    }
}
