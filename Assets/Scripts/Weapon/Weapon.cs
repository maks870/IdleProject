using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private float cooldown;
    [SerializeField] private WeaponBehavior behavior;
    [SerializeField] private WeaponLevel level;
    LevelUpImprove improve;
    private bool isActive = true;
    

    public void UseWeapon()
    {
        behavior.UseBehavior();
;
    }
    public void LevelUp()
    {
        level.LevelUp(improve);
    }
    void Start()
    {
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
