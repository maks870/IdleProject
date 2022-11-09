using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
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
}
