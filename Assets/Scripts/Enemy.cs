using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    private Rigidbody2D rb;
    private GameObject target;
    private bool moves = false;

    public void SetTarget(GameObject value)
    {
        moves = true;
        target = value;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (moves)
        {
            Move();
        }
    }
    private void Move()
    {
        Vector2 moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        rb.velocity = moveDirection * speed;
    }

    public void TakeDamage(int damage)
    {
        if (hp - damage > 0)
            hp -= damage;
        else
            hp = 0;
    }
}
