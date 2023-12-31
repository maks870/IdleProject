using System.Collections;
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
    private Color costTextColor;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += UpdatePurchase;
        YandexGame.SwitchLangEvent += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= UpdatePurchase;
        YandexGame.SwitchLangEvent -= UpdateText;
    }

    void Start()
    {
        purchaseButton = GetComponent<Button>();
        costTextColor = costText.color;

        for (int i = 0; i < upgrader.GetMaxStatLvl(statName); i++)
        {
            GameObject point = Instantiate(prefPoint, pointGrid);
            points.Add(point.transform.GetChild(0).GetComponent<Image>());
        }

        if (YandexGame.SDKEnabled == true)
        {
            UpdatePurchase();
        }
    }

    private void RefreshPoints()
    {
        for (int i = 0; i < points.Count; i++)
            points[i].enabled = i < currentLvl;
    }

    private void UpdateText(string str) 
    {
        UpdateText();
    }

    private void UpdatePurchase()
    {
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
            currentTranslation = currentTranslation.TrimEnd('+');
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
            currentValue = descriptionLang.currentTranslation == descriptionLang.tr
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
            //descriptionText.enabled = false;
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
            StartCoroutine(ChangeColor());
        }
        else
        {
            gold = balance;
            upgrader.Upgrade(statName);
            UpdatePurchase();
            YandexGame.savesData.gold = gold;
        }
    }

    private IEnumerator ChangeColor() 
    {
        costText.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        costText.color = costTextColor;
    }
}
