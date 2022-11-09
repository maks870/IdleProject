using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private IWeaponBehavior behavior;

    public void UseWeapon()
    {
        behavior.UseBehavior();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
