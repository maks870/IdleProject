using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : MonoBehaviour
{
    [SerializeField] private int expValue;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int GetValue => expValue;
    public Sprite GetSprite => spriteRenderer.sprite;
    public void ChangeExpPoint(int expValue, Sprite sprite)
    {
        this.expValue = expValue;
        spriteRenderer.sprite = sprite;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
