using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private BoxCollider2D[] zones;
    private void Start()
    {
        zones = GetComponentsInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //Spawner.KillEnemy(collision.GetComponent<Enemy>());
            enemy.deathEvent.Invoke();
        }

        if (collision.GetComponent<Projectile>() != null)
        {
            Destroy(collision.GetComponent<Projectile>().gameObject);
        }
    }
}
