using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int maxCoinCount = 100;
    [SerializeField] private GameObject defaultCoin;
    [SerializeField] static private int coinDropChance = 50;
    private static List<GameObject> coinObjectPull = new List<GameObject>();
    private static void AddToCoinObjectPull(GameObject coinPoint)
    {
        coinObjectPull.Add(coinPoint);
        coinPoint.SetActive(false);
    }
    private static GameObject RemoveCoinObjectFromPull(Coin coinPoint)
    {
        GameObject coinObject = coinObjectPull[0];
        coinObjectPull.Remove(coinObject);
        coinObject.GetComponent<Coin>().ChangeCoin(coinPoint.GetValue, coinPoint.GetAwardSprite);
        coinObject.SetActive(true);
        return coinObject;
    }
    public static void DropCoin(Coin coinPoint, Vector3 position)
    {
        if (coinObjectPull.Count > 0)
        {
            if (Random.Range(0, 100) < coinDropChance)
            {
                RemoveCoinObjectFromPull(coinPoint).transform.position = position + new Vector3(Random.Range(0f,1f), Random.Range(0f,1f), 0);
            }
        }
        else
        {
            //условие когда нехватает объектов в пуле
        }

    }
    public static void PickUpCoin(GameObject coin)
    {
        AddToCoinObjectPull(coin);
    }
    private void Awake()
    {
        CreateCoinObjectPull();
    }
    private void CreateCoinObjectPull()
    {
        coinObjectPull.Clear();
        for (int i = 0; i < maxCoinCount; i++)
        {
            GameObject newCoinObject = Instantiate(defaultCoin, transform.position, Quaternion.identity);
            AddToCoinObjectPull(newCoinObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() != null)
        {
            Player.instance.AddCoin(collision.GetComponent<Coin>().GetValue);

            AddToCoinObjectPull(collision.gameObject);
        }
    }
}
