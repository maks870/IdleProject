using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardPresenter : MonoBehaviour
{
    [SerializeField] private int GivingAwardsCount = 3;
    private List<IAward> awardList = new List<IAward>();

    public void GetAward(IAward award)
    {
        Player.instance.GetLevel.LevelUp(award.AwardAction);
    }
    public void DropAward()//получение награды за убийство сильного моба
    {
        //остановка игры
        //вывод на экран награды для выбора
    }
    public void GiveAwards()//получение награды после уровня
    {
        //остановка игры
        //вывод на экран награды для выбора
    }
    public List<IAward> RandomAwards()
    {
        List<IAward> newAwardsList = awardList;
        List<IAward> randomAwards = new List<IAward>();
        for (int i = 0; i < GivingAwardsCount; i++)
        {
            int rand = Random.Range(0, newAwardsList.Count);
            randomAwards.Add(newAwardsList[rand]);
            newAwardsList.RemoveAt(rand);
        }
        return randomAwards;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
