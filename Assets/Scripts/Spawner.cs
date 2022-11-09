using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    private BoxCollider2D[] zones;
    private Enemy enemy;
    private Character character;

    
    private void Start()
    {
        character = FindObjectOfType<Character>();
        zones = GetComponentsInChildren<BoxCollider2D>();
        StartCoroutine(TimerSpawn());
    }
    private void Update()
    {
        transform.position = character.transform.position;
    }

    private IEnumerator TimerSpawn() 
    {
        yield return new WaitForSeconds(0.5f);
        Spawn();
        StartCoroutine(TimerSpawn());
    }

    private void Spawn() 
    {
        foreach (BoxCollider2D collider in zones) 
        {
            float x = Random.Range(collider.transform.position.x - collider.size.x/2, collider.transform.position.x + collider.size.x / 2);
            float y = Random.Range(collider.transform.position.y - collider.size.y / 2, collider.transform.position.y + collider.size.y / 2);

            enemy = Instantiate(pref, new Vector3(x, y, -3f), Quaternion.identity).GetComponent<Enemy>();

            enemy.SetTarget(character.gameObject);
        }
    }
}
