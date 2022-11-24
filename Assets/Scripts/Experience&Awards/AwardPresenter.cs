using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterfaces;
using YG;

public class AwardPresenter : MonoBehaviour, IUpgradeble
{
    [SerializeField] private int givingAwardsCount = 3;// устанавливать в awake из YGsaves
    [SerializeField] private int maxWeaponCount = 3; // устанавливать в awake из YGsaves
    [SerializeField] private GameObject weapons;
    [SerializeField] private List<GameObject> currentWeapons = new List<GameObject>();
    [SerializeField] private Coin defaultCoin;
    [SerializeField] private int minCoinAwardValue;
    [SerializeField] private int maxCoinAwardValue;
    private List<Award> awardList = new List<Award>();
    private AwardPresenterUI presenterUI;
    public int GetAwardsCount => givingAwardsCount;
    public AwardPresenterUI SetPresenterUI { set { presenterUI = value; } }

    private void Awake()
    {
        SetDataVariables();
    }

    private void FillAwardsList()
    {
        awardList.Clear();
        currentWeapons.Clear();
        FillCurrentWeapons();

        if (currentWeapons.Count != maxWeaponCount)
        {
            Award[] awards = weapons.GetComponentsInChildren<Award>();
            foreach (Award award in awards)
            {
                if (award.Accessibility)
                {
                    awardList.Add(award);
                }
            }
        }
        else
        {
            foreach (GameObject weapon in currentWeapons)
            {
                Award award = weapon.GetComponent<Award>();
                if (award.Accessibility)
                {
                    awardList.Add(award);
                }
            }
        }
    }

    private void FillCurrentWeapons()
    {
        Weapon[] awardGameObjects = weapons.GetComponentsInChildren<Weapon>();
        for (int i = 0; i < awardGameObjects.Length; i++)
        {
            if (awardGameObjects[i].IsActive)
            {
                currentWeapons.Add(awardGameObjects[i].gameObject);
            }
        }
    }

    private Award GetCoinAward()
    {
        int randomValueCoin = Random.Range(minCoinAwardValue, maxCoinAwardValue);
        Coin coinAward = defaultCoin;
        coinAward.ChangeCoin(randomValueCoin, defaultCoin.Sprite);
        Award award = coinAward;
        return award;
    }

    public void GiveAward(Award award)
    {
        Player.instance.GetLevel.LevelUp(award.AwardAction);
    }

    public void DropAward()//получение награды за убийство сильного моба
    {

    }

    public void GiveAwards()//получение награды после уровня
    {
        presenterUI.ShowAwards();
    }

    public List<Award> GetRandomAwards()
    {
        FillAwardsList();

        int randomAwardsCount = awardList.Count < givingAwardsCount 
            ? awardList.Count 
            : givingAwardsCount;

        List<Award> newAwardsList = new List<Award>();
        newAwardsList.AddRange(awardList);
        List<Award> randomAwards = new List<Award>(randomAwardsCount);

        for (int i = 0; i < randomAwards.Capacity; i++)
        {
            int rand = Random.Range(0, newAwardsList.Count);
            randomAwards.Add(newAwardsList[rand]);
            newAwardsList.RemoveAt(rand);
        }

        if (randomAwards.Count == 0)
        {
            randomAwards.Add(GetCoinAward());
        }

        return randomAwards;
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        maxWeaponCount = (int)upgrader.GetDataVariable("slots", YandexGame.savesData.inventory);
        givingAwardsCount = (int)upgrader.GetDataVariable("awards", YandexGame.savesData.inventory);
    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.inventory[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.inventory[statName];
    }
}
