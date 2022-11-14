using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IAward
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxLevelCount = 5;
    [SerializeField] private Behavior behavior;
    [SerializeField] private Level level;
    private Action<bool> improve;
    private bool isActive = true;

    public Sprite GetAwardSprite => sprite;

    public string GetAwardName => weaponName;

    public string GetAwardDescription => description;



    public void UseWeapon()
    {
        behavior.Use();
    }
    public void AwardAction()
    {
        if (gameObject.activeInHierarchy)
            level.LevelUp(behavior.Improve);
        else
            gameObject.SetActive(true);
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
