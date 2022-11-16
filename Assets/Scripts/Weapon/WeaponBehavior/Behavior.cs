using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior : MonoBehaviour, IUpgradeble
{
    [SerializeField] protected List<StatsToImprove> statToImprove = new List<StatsToImprove>();
    public int improveMaxLevel;
    void Start()
    {
        SetDataVariables();
    }
    public virtual void ActiveBehavior()
    {

    }
    public virtual void Use()
    {
        Debug.Log("����������� Use");
    }
    public virtual void Combine()
    {
        Debug.Log("����������� Combine");
    }
    public virtual void Improve(bool isMaxLevel)
    {
        Debug.Log("����������� Improve");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void SetDataVariables()
    {

    }
}
