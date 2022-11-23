using UnityEngine;
using UnityEngine.UI;
using YG;

public class Product : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private Upgrader upgrader;
    [SerializeField] private Text lvlText;
    [SerializeField] private Text costText;
    private int cost;
    private int currentLvl;
    private int maxLvl;

    private void OnEnable() => YandexGame.GetDataEvent += PurchaseUpdate;
    private void OnDisable() => YandexGame.GetDataEvent -= PurchaseUpdate;

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            PurchaseUpdate();
        }
    }

    private void PurchaseUpdate()
    {
        maxLvl = upgrader.GetMaxStatLvl(statName);
        cost = upgrader.GetStatCost(statName);
        costText.text = cost.ToString();
        currentLvl = upgrader.GetStatLvl(statName);
        lvlText.text = currentLvl.ToString();
    }
    public void Purchase()
    {
        int gold = YandexGame.savesData.gold;
        int balance = gold - cost;

        if (balance < 0)
        {
            Debug.Log("Нехватает денег бичара");
        }
        else if (currentLvl >= maxLvl - 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gold = balance;
            upgrader.Upgrade(statName);
            PurchaseUpdate();
            YandexGame.savesData.gold = gold;
            Debug.LogError("Переделать");
            //YandexGame.SaveProgress();
        }
    }
}
