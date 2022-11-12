using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{

    public virtual void UseBehavior()
    {
        Debug.Log("����������� UseBehavior");
    }
    public virtual void CombineWeapon()
    {
        Debug.Log("����������� CombineWeapon");
    }
    public virtual void ImproveWeapon(bool isMaxLevel)
    {
        Debug.Log("����������� ImproveWeapon");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
