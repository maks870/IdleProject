using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private int givingAwardsCount = 3;
    private List<IAward> awardList = new List<IAward>();
    private AwardPresenterUI presenterUI;
    public int GetAwardsCount => givingAwardsCount;
    public AwardPresenterUI SetPresenterUI { set { presenterUI = value; } }
    private void GetAwardsList()
    {
        GameObject weapons = GetComponentInChildren<Transform>().gameObject;
        IAward[] awards = weapons.GetComponentsInChildren<IAward>();
        awardList.AddRange(awards);
    }
    private void Start()
    {
        GetAwardsList();
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
        List<IAward> newAwardsList = new List<IAward>();
        newAwardsList.AddRange(awardList);
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
