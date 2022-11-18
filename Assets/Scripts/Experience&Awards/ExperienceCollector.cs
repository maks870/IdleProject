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

        presenter = transform.parent.GetComponentInChildren<AwardPresenter>();
        CreatePull();
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
    public void PickUp(GameObject expPoint)
    {
        AddToPull(expPoint);
    }
    private void CreatePull()
    {
        for (int i = 0; i < maxExpCount; i++)
        {
            GameObject newExpObject = Instantiate(defaultExpPoint, transform.position, Quaternion.identity);
            AddToPull(newExpObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ExpPoint>() != null)
        {
            GetExperience(collision.GetComponent<ExpPoint>().GetValue);
            AddToPull(collision.gameObject);
        }
    }
    private void GetExperience(int expPoint)
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
