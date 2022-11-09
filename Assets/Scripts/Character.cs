using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    protected Rigidbody2D rb;
    protected Vector3 moveDirection;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        Move();
    }

    public void TakeDamage(int damage)
    {
        if (hp - damage > 0)
            hp -= damage;
        else
            hp = 0;
    }
    private void Move()
    {
        rb.velocity = moveDirection * speed;
    }
}
