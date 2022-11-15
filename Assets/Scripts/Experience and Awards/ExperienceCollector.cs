using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCollector : MonoBehaviour
{
    [SerializeField] private int maxExpCount = 100;
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experienceToLvlup = 100;
    [SerializeField] private double lvlExpMultiply = 1.3;
    [SerializeField] private AwardPresenter presenter;
    [SerializeField] private GameObject defaultExpPoint;
    private static List<GameObject> expObjectPull = new List<GameObject>();

    public static void DropExpPoint(ExpPoint expPoint, Vector3 position)
    {
        //if (visibleExpList.Count + invisibleExpList.Count < maxExpCount)
        //{
        //    visibleExpList.Add(Instantiate(expPoint, position, quaternionRotation));

        //}
        //else if (invisibleExpList.Count != 0)
        //{
        //    expObject = invisibleExpList[0];
        //    invisibleExpList.Remove(expObject);
        //    visibleExpList.Add(expObject);
        //    expObject.transform.position = position;
        //    expObject.SetActive(true);
        //}
        //else
        //{
        //    условие когда нехватает объектов в пуле
        //}
        if (expObjectPull.Count > 0)
        {
            RemoveExpObjectFromPull(expPoint).transform.position = position;
        }
        else
        {
            //условие когда нехватает объектов в пуле
        }

    }
    private static void AddToExpObjectPull(GameObject expPoint)
    {
        expObjectPull.Add(expPoint);
        expPoint.SetActive(false);
    }
    private static GameObject RemoveExpObjectFromPull(ExpPoint expPoint)
    {
        GameObject expObject = expObjectPull[0];
        expObjectPull.Remove(expObject);
        expObject.GetComponent<ExpPoint>().ChangeExpPoint(expPoint.GetValue, expPoint.GetSpriteRenderer.sprite);
        expObject.SetActive(true);
        return expObject;
    }
    private void Awake()
    {
        presenter = transform.parent.GetComponentInChildren<AwardPresenter>();
        CreateExpObjectPull();
    }
    private void CreateExpObjectPull()
    {
        for (int i = 0; i < maxExpCount; i++)
        {
            GameObject newExpObject = Instantiate(defaultExpPoint, transform.position, Quaternion.identity);
            AddToExpObjectPull(newExpObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ExpPoint>() != null)
        {
            GetExperiencePoint(collision.GetComponent<ExpPoint>().GetValue);
            AddToExpObjectPull(collision.gameObject);
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
