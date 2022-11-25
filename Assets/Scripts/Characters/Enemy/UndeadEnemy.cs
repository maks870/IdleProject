using System.Collections;
using UnityEngine;

public class UndeadEnemy : Enemy
{
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isDyingProcess = false;
    [SerializeField] private Sprite firstSprite;
    [SerializeField] private Sprite secondSprite;
    [SerializeField] private Sprite thirdSprite;
    [SerializeField] private float blinkDelay;
    [SerializeField] private Color invincibleColor;
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
        spriteRenderer.color = invincibleColor;
        for (float i = 0; i < actionCooldown; i += blinkDelay * 2)
        {
            spriteRenderer.sprite = firstSprite;
            yield return new WaitForSeconds(blinkDelay);
            spriteRenderer.sprite = secondSprite;
            yield return new WaitForSeconds(blinkDelay);
        }
        spriteRenderer.color = color;
        spriteRenderer.sprite = thirdSprite;
        hp = baseHp;
        boxCollider.isTrigger = false;
        isDyingProcess = false;
        isDead = true;
    }
}
