using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : Enemy
{
    [SerializeField] private Sprite armoredSprite;
    [SerializeField] private Sprite nonArmoredSprite;
    private bool isArmored = true;

    private void ArmorOn()
    {
        spriteRenderer.sprite = armoredSprite;
        isArmored = true;
        speed /= speedMultiplier;
    }

    private void ArmorOff()
    {
        spriteRenderer.sprite = nonArmoredSprite;
        isArmored = false;
        speed *= speedMultiplier;
        StartCoroutine(ReloadArmor(actionCooldown));
    }

    public override void TakeDamage(int damage)
    {
        if (!isArmored)
            base.TakeDamage(damage);
        else
            ArmorOff();
    }

    IEnumerator ReloadArmor(float time)
    {
        yield return new WaitForSeconds(time);
        ArmorOn();
    }
}
