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
    private List<IAward> awardList = new List<IAward>();
    private AwardPresenterUI presenterUI;
    public int GetAwardsCount => givingAwardsCount;
    public AwardPresenterUI SetPresenterUI { set { presenterUI = value; } }
    private void GetAwardsList()
    {
        awardList.Clear();
        currentWeapons.Clear();
        CountUpCurrentWeapons();
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
    private void CountUpCurrentWeapons()
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
    private void Start()
    {
    }
    public void GetAward(IAward award)
    {
        Player.instance.GetLevel.LevelUp(award.AwardAction);
    }
    public void DropAward()//��������� ������� �� �������� �������� ����
    {

    }
    public void GiveAwards()//��������� ������� ����� ������
    {
        presenterUI.ShowAwards();
    }
    public List<IAward> RandomAwards()
    {

        GetAwardsList();
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
        return randomAwards;
    }
}