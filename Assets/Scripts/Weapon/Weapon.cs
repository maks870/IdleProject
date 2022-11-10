using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private float cooldown;
    [SerializeField] private bool isActive = true;
    [SerializeField] private WeaponBehavior behavior;

    public void UseWeapon()
    {
        behavior.UseBehavior();
;
    }
    void Start()
    {
        StartCoroutine(WeaponTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator WeaponTimer()
    {
        while (isActive)
        {
            UseWeapon();
            yield return new WaitForSeconds(cooldown);
        }
    }
}
