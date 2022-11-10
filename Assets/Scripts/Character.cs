using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    protected Rigidbody2D rb;
    protected Vector3 moveDirection;
    private SpriteRenderer spriteRenderer;
    private Vector2 oldPos;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldPos = transform.position;
    }

    protected void Update()
    {
        Move();
        CheckPosition();
    }

    public void TakeDamage(int damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
<<<<<<< HEAD
        StartCoroutine(TimerDamageSprite());
=======
            Destroy(gameObject);
        }
        StartCoroutine(TimerDamage());
>>>>>>> 6e001f3e69c776cd4d39353d53c88c265714d440
    }

    private IEnumerator TimerDamageSprite()
    {
        spriteRenderer.material.SetFloat("_White", 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.SetFloat("_White", 0);
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
}
