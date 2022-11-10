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
        yield return new WaitForSeconds(0.3f);
        Spawn();
        StartCoroutine(TimerSpawn());
    }

    private void Spawn()
    {
        int i = Random.Range(0, zones.Length);

        float x = Random.Range(zones[i].transform.position.x - zones[i].size.x / 2, zones[i].transform.position.x + zones[i].size.x / 2);
        float y = Random.Range(zones[i].transform.position.y - zones[i].size.y / 2, zones[i].transform.position.y + zones[i].size.y / 2);

        enemy = Instantiate(pref, new Vector3(x, y, -3f), Quaternion.identity).GetComponent<Enemy>();

        enemy.SetTarget(character.gameObject);

    }
}