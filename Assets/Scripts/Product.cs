using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Product : MonoBehaviour, IProduct
{
    [SerializeField] private string statName;
    [SerializeField] private int playerGold;
    [SerializeField] private int goldToUpgrade;
    [SerializeField] private int currentLvl;
    [SerializeField] private Upgrader upgrader;

    void Start()
    {
        PurchaseUpdate();
        PlayerGoldGet();
    }

    private void PurchaseUpdateCost()
    {
        goldToUpgrade = upgrader.GetStatCost(statName, currentLvl);
    }
    private void PurchaseUpdateLvl()
    {
        currentLvl = upgrader.GetStatLvl(statName);
    }
    private void PlayerGoldSet()
    {
        Debug.Log("Остаток деняг " + playerGold);
        //YandexGame.savesData.gold = playerGold;
    }
    private void PlayerGoldGet()
    {
        playerGold = YandexGame.savesData.gold;
    }

    public void Purchase()
    {
        int balance = playerGold - goldToUpgrade;
        if (balance >= 0)
        {
            playerGold = balance;
            upgrader.Upgrade(statName);
            PurchaseUpdate();
            PlayerGoldUpdate();
        }
        else
        {
            Debug.Log("Нехватает денег бичара");
        }


    }
    public void PurchaseUpdate()
    {
        PurchaseUpdateLvl();
        PurchaseUpdateCost();
    }
    public void PlayerGoldUpdate()
    {
        PlayerGoldSet();
        PlayerGoldGet();
    }

}
