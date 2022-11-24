using UnityEngine;
using UnityEngine.UI;
using YG;

public class Product : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private bool percentageValue = false;
    [SerializeField] private Upgrader upgrader;
    [SerializeField] private Text lvlText;
    [SerializeField] private Text costText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private LanguageYG descriptionLang;
    [SerializeField] private LanguageYG maxCostLang;
    [SerializeField] private Button purchaseButton;
    private int cost;
    private int currentLvl;
    private int maxLvl;
    private bool isMaxLvl = false;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += UpdatePurchase;
        YandexGame.SwitchLangEvent += UpdateText;
        UpdatePurchase();
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= UpdatePurchase;
        YandexGame.SwitchLangEvent -= UpdateText;
    }

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            UpdatePurchase();
        }
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

        lvlText.text = currentLvl.ToString();

        if (isMaxLvl)
        {
            maxCostLang.enabled = true;
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

            descriptionText.text = $"{currentTranslation}{currentValue}";
        }
        else
        {
            descriptionText.text = $"{currentTranslation}{currentValue}";
        }
    }

    private void CheckMaxLevel()
    {
        if (currentLvl == maxLvl)
        {
            isMaxLvl = true;
            purchaseButton.enabled = false;
        }
        else
        {
            isMaxLvl = false;
        }
    }

    private void UpdateText(string lang)
    {
        UpdateText();
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
            Debug.LogError("Переделать");
            //YandexGame.SaveProgress();
        }
    }
}
