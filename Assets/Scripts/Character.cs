using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator animator;
    private Vector3 moveDirection;
    private Rigidbody2D rb;
    [SerializeField]private List<Enemy> enemys = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(TimerAttack());
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
    }

    private IEnumerator TimerAttack() 
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Attack");
        StartCoroutine(TimerAttack());
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemys.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            foreach (Enemy enemy in enemys)
            {
                if (enemy == collision.GetComponent<Enemy>())
                    enemys.Remove(enemy);
            }
        }
    }

    private void Attack() 
    {
        foreach (Enemy enemy in enemys)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void Move() 
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveDirection.normalized);
        rb.velocity = moveDirection * speed;
    }
}
