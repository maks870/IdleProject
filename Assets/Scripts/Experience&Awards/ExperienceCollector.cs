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

    private static void AddToExpObjectPull(GameObject expPoint)
    {
        expObjectPull.Add(expPoint);
        expPoint.SetActive(false);
    }
    private static GameObject RemoveExpObjectFromPull(ExpPoint expPoint)
    {
        GameObject expObject = expObjectPull[0];
        expObjectPull.Remove(expObject);
        expObject.GetComponent<ExpPoint>().ChangeExpPoint(expPoint.GetValue, expPoint.GetSprite);
        expObject.SetActive(true);
        return expObject;
    }
    public static void DropExpPoint(ExpPoint expPoint, Vector3 position)
    {

        if (expObjectPull.Count > 0)
        {
            RemoveExpObjectFromPull(expPoint).transform.position = position;
        }
        else
        {
            //условие когда нехватает объектов в пуле
        }
    }
    public static void PickUpExpPoint(GameObject expPoint)
    {
        AddToExpObjectPull(expPoint);
    }
    private void Awake()
    {
        presenter = transform.parent.GetComponentInChildren<AwardPresenter>();
        CreateExpObjectPull();
        Debug.Log(expObjectPull.Count);
    }
    private void CreateExpObjectPull()
    {
        expObjectPull.Clear();
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
