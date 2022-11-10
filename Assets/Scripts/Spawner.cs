using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Round
{
    public int countEnemy;
    public GameObject prefEnemy;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private Round[] rounds;
    [SerializeField] private int maxEnemy;
    [SerializeField] private GameObject pref;
    private int roundNumber = 0;
    private BoxCollider2D[] zones;
    private Enemy enemy;
    private Character character;
    private List<Enemy> spawnedEnemys = new List<Enemy>();
    public static Spawner instance = null;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        zones = GetComponentsInChildren<BoxCollider2D>();
        StartCoroutine(TimerSpawn());

        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        transform.position = character.transform.position;
    }

    private IEnumerator TimerSpawn()
    {
        yield return new WaitForSeconds(0.3f);

        if (spawnedEnemys.Count < maxEnemy)
        {
            Spawn();
        }
        StartCoroutine(TimerSpawn());
    }

    private void Spawn()
    {
        int i = Random.Range(0, zones.Length);

        float x = Random.Range(zones[i].transform.position.x - zones[i].size.x / 2, zones[i].transform.position.x + zones[i].size.x / 2);
        float y = Random.Range(zones[i].transform.position.y - zones[i].size.y / 2, zones[i].transform.position.y + zones[i].size.y / 2);

        enemy = Instantiate(rounds[roundNumber].prefEnemy, new Vector3(x, y, 0f), Quaternion.identity).GetComponent<Enemy>();
        enemy.SetTarget(character.gameObject);
        spawnedEnemys.Add(enemy);

        rounds[roundNumber].countEnemy--;
        if (rounds[roundNumber].countEnemy == 0 && roundNumber + 1 < rounds.Length)
            roundNumber++;
    }

    public static void KillEnemy(Enemy enemyDes)
    {
        instance.spawnedEnemys.Remove(enemyDes);
        Destroy(enemyDes.gameObject);
    }
}