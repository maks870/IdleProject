using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IAward
{
    [SerializeField] private string weaponName;
    [SerializeField] private IUpgradeble IImprovable;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxLevelCount = 5;
    [SerializeField] private Behavior behavior;
    [SerializeField] private Level level;
    [SerializeField] private bool isActive = false;
    private Action<bool> improve;
    public Sprite GetAwardSprite => sprite;

    public string GetAwardName => weaponName;

    public string GetAwardDescription => description;

    public bool GetAwardAccessibility => !level.IsMaxLevel;

    public bool IsWeaponActive => isActive;

    public void UseWeapon()
    {
        behavior.Use();
    }
    public void AwardAction()
    {
        if (isActive)
            level.LevelUp(behavior.Improve);
        else
        {
            isActive = true;
            behavior.ActiveBehavior();
            StartCoroutine(WeaponTimer());
        }
        //gameObject.SetActive(true);
    }
    void Start()
    {
        level = new Level(maxLevelCount);
        improve += behavior.Improve;
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
