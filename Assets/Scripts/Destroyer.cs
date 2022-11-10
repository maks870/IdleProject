using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Character character;
    private BoxCollider2D[] zones;
    private void Start()
    {
        character = FindObjectOfType<Character>();
        zones = GetComponentsInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position = character.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            Spawner.KillEnemy(collision.GetComponent<Enemy>());
    }
}
