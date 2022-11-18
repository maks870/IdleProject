using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int maxCoinCount = 100;
    [SerializeField] private GameObject defaultCoin;
    [SerializeField] static private int coinDropChance = 50;
    private List<GameObject> invisiblePull = new List<GameObject>();
    private List<GameObject> visiblePull = new List<GameObject>();
    public static CoinCollector instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        CreateCoinObjectPull();
    }
    private void AddToPull(GameObject coinPoint)
    {
        invisiblePull.Add(coinPoint);
        visiblePull.Remove(coinPoint);
        coinPoint.SetActive(false);
    }
    private GameObject RemoveFromPull(Coin coinPoint)
    {
        GameObject coinObject = invisiblePull[0];
        invisiblePull.Remove(coinObject);
        visiblePull.Add(coinObject);
        coinObject.GetComponent<Coin>().ChangeCoin(coinPoint.GetValue, coinPoint.GetAwardSprite);
        coinObject.SetActive(true);
        return coinObject;
    }
    public void Drop(Coin coinPoint, Vector3 position)
    {
        if (invisiblePull.Count > 0)
        {
            if (Random.Range(0, 100) < coinDropChance)
            {
                RemoveFromPull(coinPoint).transform.position = position + new Vector3(Random.Range(0f,1f), Random.Range(0f,1f), 0);
            }
        }
        else
        {
            GameObject coinObject = visiblePull[0];
            visiblePull.Remove(coinObject);
            visiblePull.Add(coinObject);
            coinObject.transform.position = position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0); ;
        }

    }
    public void PickUp(GameObject coin)
    {
        AddToPull(coin);
    }
    private void CreateCoinObjectPull()
    {
        for (int i = 0; i < maxCoinCount; i++)
        {
            GameObject newCoinObject = Instantiate(defaultCoin, transform.position, Quaternion.identity);
            AddToPull(newCoinObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() != null)
        {
            Player.instance.AddCoin(collision.GetComponent<Coin>().GetValue);

            AddToPull(collision.gameObject);
        }
    }
}
