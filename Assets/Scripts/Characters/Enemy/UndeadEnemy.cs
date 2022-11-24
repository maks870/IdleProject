using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadEnemy : Enemy
{
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isDyingProcess = false;
    [SerializeField] private Sprite firstSprite;
    [SerializeField] private Sprite secondSprite;
    private Collider2D boxCollider;
    private int baseHp;
    private float baseSpeed;

    protected override void Start()
    {
        base.Start();
        baseHp = hp;
        baseSpeed = speed;
        boxCollider = GetComponent<Collider2D>();
        spriteRenderer.sprite = firstSprite;
    }
    protected override void Update()
    {
        base.Update();
        if (isDyingProcess)
            speed = 0;
        else
            speed = baseSpeed;
    }

    public override void TakeDamage(int damage)
    {
        if (!isDyingProcess)
            base.TakeDamage(damage);
    }

    protected override void Dead()
    {
        if (!isDead)
            StartCoroutine(DyingProcess(actionCooldown));
        else
            base.Dead();
    }

    IEnumerator DyingProcess(float time)
    {
        isDyingProcess = true;
        boxCollider.isTrigger = true;
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(255, 181, 0, 255);

        yield return new WaitForSeconds(time);

        spriteRenderer.color = color;
        hp = baseHp;
        boxCollider.isTrigger = false;
        spriteRenderer.sprite = secondSprite;
        isDyingProcess = false;
        isDead = true;
    }
}
