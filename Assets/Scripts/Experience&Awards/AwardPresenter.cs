using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterfaces;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private int givingAwardsCount = 3;
    [SerializeField] private int maxWeaponCount = 4;
    [SerializeField] private GameObject weapons;
    [SerializeField] private List<GameObject> currentWeapons = new List<GameObject>();
    [SerializeField] private Coin defaultCoin;
    [SerializeField] private int minCoinAwardValue;
    [SerializeField] private int maxCoinAwardValue;
    private List<IAward> awardList = new List<IAward>();
    private AwardPresenterUI presenterUI;
    public int GetAwardsCount => givingAwardsCount;
    public AwardPresenterUI SetPresenterUI { set { presenterUI = value; } }
    private void FillAwardsList()
    {
        awardList.Clear();
        currentWeapons.Clear();
        FillCurrentWeapons();
        if (currentWeapons.Count != maxWeaponCount)
        {
            IAward[] awards = weapons.GetComponentsInChildren<IAward>();
            foreach (IAward award in awards)
            {
                if (award.GetAwardAccessibility)
                {
                    awardList.Add(award);
                }
            }
        }
        else
        {
            IAward award;
            foreach (GameObject weapon in currentWeapons)
            {
                award = weapon.GetComponent<IAward>();
                if (award.GetAwardAccessibility)
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
            if (awardGameObjects[i].IsWeaponActive)
            {
                currentWeapons.Add(awardGameObjects[i].gameObject);
            }
        }
    }
    private IAward GetCoinAward()
    {
        int randomValueCoin = Random.Range(minCoinAwardValue, maxCoinAwardValue);
        Coin coinAward = defaultCoin;
        coinAward.ChangeCoin(randomValueCoin, defaultCoin.GetAwardSprite);
        IAward award = coinAward;
        return award;
    }
    public void GiveAward(IAward award)
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
    public List<IAward> GetRandomAwards()
    {

        FillAwardsList();
        int randomAwardsCount = awardList.Count < givingAwardsCount ? awardList.Count : givingAwardsCount;
        List<IAward> newAwardsList = new List<IAward>();
        newAwardsList.AddRange(awardList);
        List<IAward> randomAwards = new List<IAward>(randomAwardsCount);
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
}
