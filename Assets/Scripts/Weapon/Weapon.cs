using System;
using System.Collections;
using UnityEngine;
using YG;

public class Weapon : MonoBehaviour, IAward
{
    [SerializeField] private IUpgradeble IImprovable;
    [SerializeField] private LanguageYG weaponName;
    [SerializeField] private LanguageYG description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxLevelCount = 5;
    [SerializeField] private Behavior behavior;
    [SerializeField] private Level level;
    //[SerializeField] private Translations translations = new Translations(2);
    private bool isActive = false;
    private Action<bool> improve;
    public Sprite GetAwardSprite => sprite;

    public string GetAwardName => weaponName.currentTranslation;

    public string GetAwardDescription => description.currentTranslation;

    public bool GetAwardAccessibility => !level.IsMaxLevel;

    public bool IsWeaponActive => isActive;

    void Awake()
    {
        level = new Level(maxLevelCount);
        improve += behavior.Improve;
    }
    void Start()
    {
    }
    private void ActiveWeapon()
    {
        isActive = true;
        behavior.ActiveBehavior();
        StartCoroutine(WeaponTimer());
    }
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
            ActiveWeapon();
        }
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
