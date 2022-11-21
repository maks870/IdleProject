using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed; //убрать speed для базового класса, а вообще можно переделать типо с помощью стратегии
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
