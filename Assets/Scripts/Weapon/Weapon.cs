using System.Collections;
using UnityEngine;

public class Weapon : Award
{
    [SerializeField] private IUpgradeble IImprovable;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxLevelCount = 5;
    private Behavior behavior;
    private Level level;
    private bool isActive = false;
    public bool IsActive  => isActive;

    public override string Description => isActive ? base.Description : Name;
    public override bool Accessibility => !level.IsMaxLevel;

    void Awake()
    {
        behavior = GetComponent<Behavior>();
        level = new Level(maxLevelCount);
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

    IEnumerator WeaponTimer()
    {
        while (isActive)
        {
            UseWeapon();
            yield return new WaitForSeconds(cooldown);
        }
    }

    public override void AwardAction()
    {
        if (isActive) 
            level.LevelUp(behavior.Improve);
        else
            ActiveWeapon();
    }
}
