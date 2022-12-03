using System.Collections.Generic;
using UnityEngine;
using YG;

public class ExperienceCollector : MonoBehaviour, IUpgradeble
{
    [SerializeField] private int maxExpObj = 100;
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experienceToLvlup = 100;
    [SerializeField] private int multiplySteps = 10;


    [SerializeField] private double simpleLvlExpMultiply = 4;

    [SerializeField] private int currentLvl = 0;
    [SerializeField] private double startLvlExpMultiply = 2;
    [SerializeField] private double endLvlExpMultiply = 1.3;

    [SerializeField] private GameObject expPref;
    [SerializeField] private AwardPresenter presenter;
    private float expMultiplier;
    private List<GameObject> invisiblePull = new List<GameObject>();
    private List<GameObject> visiblePull = new List<GameObject>();

    public static ExperienceCollector instance = null;

    public int CurrentExp => currentExperience;
    public int ExpToLvlup => experienceToLvlup;

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
        SetDataVariables();
        CreatePull();
    }

    private void Update()
    {
        CheckLvlup();
    }

    private void AddToPull(GameObject expPoint)
    {
        invisiblePull.Add(expPoint);
        visiblePull.Remove(expPoint);
        expPoint.SetActive(false);
    }

    private GameObject RemoveFromPull(ExpPoint expPoint)
    {
        GameObject expObject = invisiblePull[0];
        invisiblePull.Remove(expObject);
        visiblePull.Add(expObject);
        expObject.GetComponent<ExpPoint>().ChangeExpPoint(expPoint.GetValue, expPoint.GetSprite);
        expObject.SetActive(true);
        return expObject;
    }

    public void Drop(ExpPoint expPoint, Vector3 position)
    {

        if (invisiblePull.Count > 0)
        {
            RemoveFromPull(expPoint).transform.position = position;
        }
        else
        {
            GameObject expObject = visiblePull[0];
            visiblePull.Remove(expObject);
            visiblePull.Add(expObject);
            expObject.transform.position = position;
        }
    }

    private void CreatePull()
    {
        for (int i = 0; i < maxExpObj; i++)
        {
            GameObject newExpObject = Instantiate(expPref, transform.position, Quaternion.identity);
            AddToPull(newExpObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ExpPoint>() != null)
        {
            currentExperience += (int)(collision.GetComponent<ExpPoint>().GetValue * expMultiplier);
            AddToPull(collision.gameObject);
        }
    }

    private void CheckLvlup()
    {
        if (currentExperience >= experienceToLvlup)
        {
            currentExperience -= experienceToLvlup;
            if (currentExperience >= experienceToLvlup)
            {
                Debug.LogError("Двойной LVLUP, двойная пауза");
            }
            experienceToLvlup = CountExpMultiply();
            //experienceToLvlup = CountExpSimple();
            currentLvl++;
            presenter.GiveAwards();
        }
    }
    private int CountExpMultiply()// счет опыта с множителем по колличеству опыта
    {
        if (currentLvl < multiplySteps)
        {
            double step = (startLvlExpMultiply - endLvlExpMultiply) / multiplySteps;
            int expToLvlUp = (int)(experienceToLvlup * (startLvlExpMultiply - step * currentLvl));
            return expToLvlUp;
        }
        return (int)(experienceToLvlup * endLvlExpMultiply);
    }
    private int CountExpSimple()// счет опыта с множителем по уровню
    {
        return (int)((currentLvl + 1) * simpleLvlExpMultiply * experienceToLvlup);
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        expMultiplier = (upgrader.GetBaseValue("experienceMultiplier") * upgrader.GetValue("experienceMultiplier") / 100);
    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.collector.AddValue(statName, 1);
    }
    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.collector.GetValue(statName);
    }
}
