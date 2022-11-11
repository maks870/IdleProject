using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{

    public virtual void UseBehavior()
    {
        Debug.Log("Абстрактный UseBehavior");
    }
    public virtual void CombineWeapon()
    {
        Debug.Log("Абстрактный CombineWeapon");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
