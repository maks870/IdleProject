using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SlayBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float duration;
    [SerializeField] private SlayProjectile slayProjectile;
    private SpriteRenderer spriteRenderer;
    private bool isSlaying = false;
    public bool IsSlaying => isSlaying;

    private void Update()
    {
        if (isSlaying)
        {
            float angle = Vector3.SignedAngle(Vector3.up, Player.instance.GetMoveDirection.normalized, Vector3.forward);
            if (angle <= 0) spriteRenderer.flipY = false;
            else spriteRenderer.flipY = true;
            slayProjectile.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
    public override void Combine()
    {
        //логика объединения оружия
    }

    public override void ActiveBehavior()
    {
        slayProjectile = Instantiate(projectile, transform).GetComponent<SlayProjectile>();
        slayProjectile.GetComponent<SlayProjectile>().slayBehavior = this;
        spriteRenderer = slayProjectile.GetComponent<SpriteRenderer>();
        slayProjectile.gameObject.SetActive(false);
    }
    public override void Use()
    {
        StartCoroutine(Timer());
    }
    public override void Improve(bool isMaxLevel)
    {
        Debug.Log("Стрельба улучшена");
        //метод улучщения оружия
    }
    public override void SetDataVariables()
    {
        Debug.Log("установка начальных значений");
        Upgrader upgrader = GetComponent<Upgrader>();
    }
    public override void Upgrade(string statName)
    {
        YandexGame.savesData.explosiveWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.explosiveWeapon[statName];
    }

    IEnumerator Timer()
    {
        slayProjectile.gameObject.SetActive(true);
        isSlaying = true;
        yield return new WaitForSeconds(duration);
        isSlaying = false;
        slayProjectile.gameObject.SetActive(false);
    }
}
