using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCollector : MonoBehaviour
{
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experienceToLvlup = 100;
    [SerializeField] private double lvlExpMultiply = 1.3;
    [SerializeField] private AwardPresenter presenter;

    private void Start()
    {
        presenter = transform.parent.GetComponentInChildren<AwardPresenter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ExpPoint>() != null)
        {
            GetExperiencePoint(collision.GetComponent<ExpPoint>().GetValue());
        }
    }
    private void GetExperiencePoint(int expPoint)
    {
        currentExperience += expPoint;
        if (currentExperience >= experienceToLvlup)
        {
            currentExperience -= experienceToLvlup;
            experienceToLvlup = (int)(experienceToLvlup * lvlExpMultiply);
            presenter.GiveAwards();
        }
    }
}
