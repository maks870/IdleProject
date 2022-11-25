using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    private Vector2 oldPos;
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    [SerializeField] private bool fly;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected Vector3 moveDirection;
    protected Animator animator;
    protected bool isDamaged = false;


    public int Hp { get => hp; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        oldPos = transform.position;
        animator.SetBool("Fly", fly);
    }

    protected virtual void Update()
    {
        Move();
        CheckPosition();
    }

    public virtual void TakeDamage(int damage)
    {
        if (hp == 0)
            return;

        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
            Dead();
        }
        StartCoroutine(TimerDamageSprite());
    }

    private IEnumerator TimerDamageSprite()
    {
        spriteRenderer.material.SetFloat("_White", 1);
        isDamaged = true;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.SetFloat("_White", 0);
        isDamaged = false;
    }

    private void Move()
    {
        rb.velocity = moveDirection * speed;
    }

    private void CheckPosition()
    {
        if (oldPos.x != transform.position.x)
        {
            if (oldPos.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            oldPos = transform.position;
        }
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
    }

    protected virtual void Dead()
    {
    }
}
