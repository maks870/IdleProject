using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Product : MonoBehaviour
{
    [SerializeField] private GameObject prefPoint;
    [SerializeField] private string statName;
    [SerializeField] private bool percentageValue = false;
    [SerializeField] private Upgrader upgrader;
    [SerializeField] private RectTransform pointGrid;
    [SerializeField] private Text costText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private LanguageYG descriptionLang;
    [SerializeField] private LanguageYG maxCostLang;
    [SerializeField] private List<Image> points = new List<Image>();
    private Button purchaseButton;
    private int cost;
    private int currentLvl;
    private int maxLvl;
    private bool isMaxLvl = false;

    void Start()
    {
        YandexGame.SwitchLangEvent += (string m) => UpdatePurchase();//есть вероятность не загрузки
        purchaseButton = GetComponent<Button>();

        for (int i = 0; i < upgrader.GetMaxStatLvl(statName); i++)
        {
            GameObject point = Instantiate(prefPoint, pointGrid);
            points.Add(point.transform.GetChild(0).GetComponent<Image>());
        }
        UpdatePurchase();
    }

    private void RefreshPoints()
    {
        for (int i = 0; i < points.Count; i++)
            points[i].enabled = i < currentLvl;
    }

    private void UpdatePurchase()
    {
        Debug.Log(11111);
        maxLvl = upgrader.GetMaxStatLvl(statName);
        currentLvl = upgrader.GetStatLvl(statName);
        cost = upgrader.GetStatCost(statName);
        CheckMaxLevel();
        UpdateText();
    }

    private void UpdateText()
    {
        string currentValue;
        string currentTranslation = descriptionLang.currentTranslation;

        RefreshPoints();

        if (isMaxLvl)
        {
            costText.text = maxCostLang.currentTranslation;
            currentValue = upgrader.GetValue(statName).ToString();
        }
        else
        {
            costText.text = cost.ToString();
            currentValue = upgrader.GetValueUpgrade(statName).ToString();
        }

        if (percentageValue)
        {
            currentValue = currentTranslation == descriptionLang.tr
                ? "%" + currentValue
                : currentValue + "%";
        }

        descriptionText.text = $"{currentTranslation}{currentValue}";
    }

    private void CheckMaxLevel()
    {
        if (currentLvl == maxLvl)
        {
            isMaxLvl = true;
            purchaseButton.enabled = false;
            descriptionText.enabled = false;
        }
        else
        {
            isMaxLvl = false;
        }
    }

    public void Purchase()
    {
        int gold = YandexGame.savesData.gold;
        int balance = gold - cost;

        if (balance < 0)
        {
            Debug.Log("Нехватает денег бичара");
        }
        else
        {
            gold = balance;
            upgrader.Upgrade(statName);
            UpdatePurchase();
            YandexGame.savesData.gold = gold;
        }
    }
}
