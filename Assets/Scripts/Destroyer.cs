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
            Spawner.KillEnemy(collision.GetComponent<Enemy>());
    }
}
