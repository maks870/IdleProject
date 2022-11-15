using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int GetValue => coinValue;
    public SpriteRenderer GetSpriteRenderer { get { return spriteRenderer; } }
    public void ChangeExpPoint(int coinValue, Sprite sprite)
    {
        this.coinValue = coinValue;
        spriteRenderer.sprite = sprite;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
