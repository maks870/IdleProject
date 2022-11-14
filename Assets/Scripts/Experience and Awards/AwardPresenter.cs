using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private int givingAwardsCount = 3;
    [SerializeField] private int maxWeaponCount = 4;
    [SerializeField] private List<GameObject> currentWeapons = new List<GameObject>();
    private List<IAward> awardList = new List<IAward>();
    private AwardPresenterUI presenterUI;
    public int GetAwardsCount => givingAwardsCount;
    public AwardPresenterUI SetPresenterUI { set { presenterUI = value; } }
    private void GetAwardsList()
    {
        GameObject allWeapons;
        awardList.Clear();
        currentWeapons.Clear();
        currentWeapons.AddRange(CountUpCurrentWeapons(out allWeapons));
        if (currentWeapons.Count != maxWeaponCount)
        {
            IAward[] awards = allWeapons.GetComponentsInChildren<IAward>();
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
    private List<GameObject> CountUpCurrentWeapons(out GameObject weapons)
    {
        List<GameObject> currentWeapons = new List<GameObject>();
        weapons = GetComponentInChildren<Transform>().gameObject;
        Weapon[] awardGameObjects = weapons.GetComponentsInChildren<Weapon>();
        Debug.Log("ќбщее колличсесво оружи€ " + awardGameObjects.Length);
        for (int i = 0; i < awardGameObjects.Length; i++)
        {
            if (awardGameObjects[i].IsWeaponActive)
            {
                currentWeapons.Add(awardGameObjects[i].gameObject);
            }
        }
        Debug.Log(" олличество текущего оружи€ "+ currentWeapons.Count);
        return currentWeapons;
    }
    private void Start()
    {
        //GetAwardsList();
    }
    public void GetAward(IAward award)
    {
        Player.instance.GetLevel.LevelUp(award.AwardAction);
    }
    public void DropAward()//получение награды за убийство сильного моба
    {

    }
    public void GiveAwards()//получение награды после уровн€
    {
        presenterUI.ShowAwards();
    }
    public List<IAward> RandomAwards()
    {
        GetAwardsList();
        List<IAward> newAwardsList = new List<IAward>();
        newAwardsList.AddRange(awardList);
        Debug.Log(awardList.Count);
        List<IAward> randomAwards = new List<IAward>(givingAwardsCount);
        for (int i = 0; i < randomAwards.Capacity; i++)
        {

            int rand = Random.Range(0, newAwardsList.Count);
            randomAwards.Add(newAwardsList[rand]);
            newAwardsList.RemoveAt(rand);
        }
        Debug.Log(randomAwards.Capacity);
        return randomAwards;
    }
}
