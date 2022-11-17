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
    [SerializeField] private int maxLvl;
    [SerializeField] private Upgrader upgrader;

    void Start()
    {
        PurchaseUpdate();
        PlayerGoldGet();
        GetMaxStatLvl();
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
        Debug.Log("������� ����� " + playerGold);
        //YandexGame.savesData.gold = playerGold;
    }
    private void PlayerGoldGet()
    {
        playerGold = YandexGame.savesData.gold;
    }
    private void GetMaxStatLvl()
    {
        maxLvl = upgrader.GetMaxStatLvl(statName);
    }
    public void Purchase()
    {
        int balance = playerGold - goldToUpgrade;
        if (balance >= 0 && currentLvl < maxLvl)
        {
            playerGold = balance;
            upgrader.Upgrade(statName);
            PurchaseUpdate();
            PlayerGoldUpdate();
        }
        else
        {
            Debug.Log("��������� ����� ������");
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
