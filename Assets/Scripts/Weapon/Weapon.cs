using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IAward
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxLevelCount = 5;
    [SerializeField] private WeaponBehavior behavior;
    [SerializeField] private WeaponLevel level;
    private Action<bool> improve;
    private bool isActive = true;
    

    public void UseWeapon()
    {
        behavior.UseBehavior();
    }
    public void AwardAction()
    {
        level.LevelUp(behavior.ImproveWeapon);
    }
    void Start()
    {
        level = new WeaponLevel(maxLevelCount);
        improve += behavior.ImproveWeapon;
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
