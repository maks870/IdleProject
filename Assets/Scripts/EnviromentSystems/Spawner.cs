using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class SpawnEnemy
{
    public GameObject pref;
    [Range(0, 100)]
    public float probability;
}

[Serializable]
public class Round
{
    public int countEnemy;
    public SpawnEnemy[] spawnEnemies;
    [HideInInspector] public float probabilityAll;
    public float spawnTime = 0.3f;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private int roundNumber = 0;
    [SerializeField] private Round[] rounds;
    [SerializeField] private int maxEnemy;

    private BoxCollider2D[] zones;
    private Enemy enemy;
    private List<Enemy> spawnedEnemies = new List<Enemy>();
    public GameObject targetObject;
    public static Spawner instance = null;

    public int CurrentRound => roundNumber;

    private void Start()
    {
        if (targetObject == null)
        {
            targetObject = Player.instance.gameObject;
        }

        zones = GetComponentsInChildren<BoxCollider2D>();
        StartCoroutine(TimerSpawn());

        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        foreach (Round round in rounds)
        {
            float probAll = 0;

            foreach (SpawnEnemy spawnEnemy in round.spawnEnemies)
            {
                probAll += spawnEnemy.probability;
            }

            round.probabilityAll = probAll;
        }
    }

    private IEnumerator TimerSpawn()
    {
        if (spawnedEnemies.Count < maxEnemy && rounds[roundNumber]?.spawnEnemies != null)
        {
            Spawn();
        }

        yield return new WaitForSeconds(rounds[roundNumber].spawnTime);
        StartCoroutine(TimerSpawn());
    }

    private void Spawn()
    {
        float probMax = rounds[roundNumber].probabilityAll;
        float prob = Random.Range(0, probMax);
        GameObject spawnObject = null;

        for (int i = 0; i < rounds[roundNumber].spawnEnemies.Length; i++)
        {
            SpawnEnemy spawnEnemy = rounds[roundNumber].spawnEnemies[i];

            if (prob <= spawnEnemy.probability)
            {
                spawnObject = spawnEnemy.pref;
                break;
            }
            else
                prob -= spawnEnemy.probability;
        }

        enemy = Instantiate(spawnObject, SpawnPosition(), Quaternion.identity).GetComponent<Enemy>();
        enemy.SetTarget(targetObject);
        spawnedEnemies.Add(enemy);
        Enemy enemyForDelegate = enemy;// создание уникалного объекта врага, так как Enemy - ссылочный тип
        enemy.deathEvent.AddListener(delegate { KillEnemy(enemyForDelegate); });// добавление слушателя метода KillEnemy() срабатывающего при смерти врага
        rounds[roundNumber].countEnemy--;

        if (rounds[roundNumber].countEnemy == 0 && roundNumber + 1 < rounds.Length)
            roundNumber++;
    }

    private Vector2 SpawnPosition()
    {
        int rand = Random.Range(0, zones.Length);

        float x = Random.Range(zones[rand].transform.position.x - zones[rand].size.x / 2, zones[rand].transform.position.x + zones[rand].size.x / 2);
        float y = Random.Range(zones[rand].transform.position.y - zones[rand].size.y / 2, zones[rand].transform.position.y + zones[rand].size.y / 2);

        return new Vector2(x, y);
    }

    public void KillEnemy(Enemy enemy) //метод для добавления в событие смерти врага
    {
        spawnedEnemies.Remove(enemy);
    }
}