using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]private List<Enemy> enemys = new List<Enemy>();

    private new void Start()
    {
        base.Start();
        StartCoroutine(TimerAttack());
    }

    private new void Update()
    {
        base.Update();
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveDirection.normalized);
    }

    private IEnumerator TimerAttack() 
    {
        yield return new WaitForSeconds(0.2f);
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
}
