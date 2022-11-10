using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject coin;
    private GameObject target;
    private Player player;   
    

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
        if(collision.GetComponent<Player>()!=null)
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
        Spawner.KillEnemy(this);
        Instantiate(coin, transform.position, Quaternion.identity);
        
    }
}
