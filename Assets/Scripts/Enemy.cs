using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int damage;
    private GameObject target;
    

    public void SetTarget(GameObject value)
    { 
        target = value;
    }
    private new void Update()
    {
        base.Update();
        moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>()?.TakeDamage(damage);
    }
}
