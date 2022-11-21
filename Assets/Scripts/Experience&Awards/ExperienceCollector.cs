using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCollector : MonoBehaviour
{
    [SerializeField] private int maxExpObj = 100;
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experienceToLvlup = 100;
    [SerializeField] private double lvlExpMultiply = 1.3;
    [SerializeField] private GameObject expPref;
    private int queue = 0;
    private AwardPresenter presenter;
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
    private void Update()
    {
        //if (queue > 0 && !MenuGame.instance.IsPaused)
        //{
        //    presenter.GiveAwards();
        //    queue--;
        //}
        CheckLvlup();
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
    private void CreatePull()
    {
        for (int i = 0; i < maxExpObj; i++)
        {
            GameObject newExpObject = Instantiate(expPref, transform.position, Quaternion.identity);
            AddToPull(newExpObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ExpPoint>() != null)
        {
            //AddExperience(collision.GetComponent<ExpPoint>().GetValue);
            currentExperience += collision.GetComponent<ExpPoint>().GetValue;
            AddToPull(collision.gameObject);
        }
    }
    //private void AddExperience(int expPoint)
    //{
    //    currentExperience += expPoint;
    //    if (currentExperience >= experienceToLvlup)//LVLUP
    //    {
    //        currentExperience -= experienceToLvlup;
    //        experienceToLvlup = (int)(experienceToLvlup * lvlExpMultiply);
    //        queue++;
    //    }
    //}  
    private void CheckLvlup()
    {
        if (currentExperience >= experienceToLvlup)
        {
            currentExperience -= experienceToLvlup;
            experienceToLvlup = (int)(experienceToLvlup * lvlExpMultiply);
            presenter.GiveAwards();
        }
    }
}
